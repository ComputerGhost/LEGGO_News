using Core.Common;
using Core.Common.Database;
using Core.Images;
using Core.Images.Operations;
using Core.Startup;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Music;
public class UpdateAlbumCommand : IRequest
{
    public int Id { get; set; }

    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public ImageUpload? AlbumArt { get; set; }

    internal interface IDatabasePort
    {
        Task<AlbumEntity?> FetchAlbum(int id);
        Task<AlbumTypeEntity> FetchAlbumType(string name);
        Task SaveChanges();
    }

    [ServiceImplementation]
    private class DatabaseAdapter : IDatabasePort
    {
        private readonly MyDbContext _dbContext;

        public DatabaseAdapter(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AlbumEntity?> FetchAlbum(int id)
        {
            return _dbContext.Albums
                .Include(album => album.AlbumType)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<AlbumTypeEntity> FetchAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(x => x.Name == name);
        }

        public Task SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }
    }

    internal class Handler : IRequestHandler<UpdateAlbumCommand>
    {
        private readonly IImagingFacade _imagingFacade;
        private readonly IDatabasePort _databaseAdapter;

        public Handler(IImagingFacade imagingFacade, IDatabasePort databaseAdapter)
        {
            _imagingFacade = imagingFacade;
            _databaseAdapter = databaseAdapter;
        }

        public async Task Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var existingAlbum = await _databaseAdapter.FetchAlbum(request.Id);
            if (existingAlbum == null)
            {
                throw new NotFoundException();
            }

            existingAlbum.AlbumType = await _databaseAdapter.FetchAlbumType(request.AlbumType.ToString());
            existingAlbum.Title = request.Title;
            existingAlbum.Artist = request.Artist;
            existingAlbum.ReleaseDate = request.ReleaseDate;

            if (request.AlbumArt != null)
            {
                existingAlbum.Image = await _imagingFacade.SaveToFileSystem(request.AlbumArt);
            }

            await _databaseAdapter.SaveChanges();
        }
    }

    internal class Validator : AbstractValidator<UpdateAlbumCommand>
    {
        public Validator(IValidator<ImageUpload> imageUploadValidator)
        {
            RuleFor(command => command.Title)
                .Length(1, 50);

            RuleFor(command => command.Artist)
                .Length(1, 50);

            RuleFor(command => command.AlbumArt)
                .SetValidator(imageUploadValidator!)
                .When(command => command.AlbumArt != null);
        }
    }
}
