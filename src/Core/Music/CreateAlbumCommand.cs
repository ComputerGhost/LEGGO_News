using Core.Common.Database;
using Core.Images;
using Core.Images.Operations;
using Core.Startup;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Music;
public class CreateAlbumCommand : IRequest<int>
{
    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public ImageUpload AlbumArt { get; set; } = null!;

    internal interface IDatabasePort
    {
        Task<int> Create(AlbumEntity albumEntity);
        Task<AlbumTypeEntity> FetchAlbumType(string name);
    }

    [ServiceImplementation]
    private class DatabaseAdapter : IDatabasePort
    {
        private readonly MyDbContext _dbContext;

        public DatabaseAdapter(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(AlbumEntity albumEntity)
        {
            _dbContext.Add(albumEntity);
            await _dbContext.SaveChangesAsync();
            return albumEntity.Id;
        }

        public Task<AlbumTypeEntity> FetchAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(x => x.Name == name);
        }
    }

    internal class Handler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IImagingFacade _imagingFacade;
        private readonly IDatabasePort _databaseAdapter;

        public Handler(IImagingFacade imagingFacade, IDatabasePort databaseAdapter)
        {
            _imagingFacade = imagingFacade;
            _databaseAdapter = databaseAdapter;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            return await _databaseAdapter.Create(new AlbumEntity
            {
                AlbumType = await _databaseAdapter.FetchAlbumType(request.AlbumType.ToString()),
                Title = request.Title,
                Artist = request.Artist,
                ReleaseDate = request.ReleaseDate,
                Image = await _imagingFacade.SaveToFileSystem(request.AlbumArt),
            });
        }
    }

    internal class Validator : AbstractValidator<CreateAlbumCommand>
    {
        public Validator(IValidator<ImageUpload> imageUploadValidator)
        {
            RuleFor(command => command.Title)
                .Length(1, 50);

            RuleFor(command => command.Artist)
                .Length(1, 50);

            RuleFor(command => command.AlbumArt)
                .NotNull()
                .SetValidator(imageUploadValidator);
        }
    }
}
