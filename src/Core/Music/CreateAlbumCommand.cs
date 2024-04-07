using Core.Common.Database;
using Core.Images;
using Core.Images.Storage;
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

    private class CommandHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IImageSaver _imageSaver;
        private readonly MyDbContext _dbContext;

        public CommandHandler(MyDbContext dbContext, IImageSaver imageSaver)
        {
            _dbContext = dbContext;
            _imageSaver = imageSaver;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var albumEntity = new AlbumEntity
            {
                AlbumType = await GetAlbumType(request.AlbumType.ToString()),
                Title = request.Title,
                Artist = request.Artist,
                ReleaseDate = request.ReleaseDate,
                Image = await _imageSaver.Create(request.AlbumArt),
            };

            _dbContext.Add(albumEntity);
            await _dbContext.SaveChangesAsync();

            return albumEntity.Id;
        }

        private Task<AlbumTypeEntity> GetAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(albumType => albumType.Name == name);
        }
    }

    private class CommandValidator : AbstractValidator<CreateAlbumCommand>
    {
        public CommandValidator(IValidator<ImageUpload> imageUploadValidator)
        {
            RuleFor(command => command.Title)
                .MaximumLength(50);

            RuleFor(command => command.Artist)
                .MaximumLength(50);

            RuleFor(command => command.AlbumArt)
                .NotNull()
                .SetValidator(imageUploadValidator);
        }
    }
}
