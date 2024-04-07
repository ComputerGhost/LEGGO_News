using Core.Common.Database;
using Core.Images;
using Core.Images.Storage;
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

    private class CommandHandler : IRequestHandler<UpdateAlbumCommand>
    {
        private readonly IImageSaver _imageStore;
        private readonly MyDbContext _dbContext;

        public CommandHandler(MyDbContext dbContext, IImageSaver imageStore)
        {
            _dbContext = dbContext;
            _imageStore = imageStore;
        }

        public async Task Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var existingAlbum = await GetAlbum(request.Id);
            existingAlbum.AlbumType = await GetAlbumType(request.AlbumType.ToString());
            existingAlbum.Title = request.Title;
            existingAlbum.Artist = request.Artist;
            existingAlbum.ReleaseDate = request.ReleaseDate;

            if (request.AlbumArt != null)
            {
                existingAlbum.Image = await _imageStore.Create(request.AlbumArt);
            }

            await _dbContext.SaveChangesAsync();
        }

        private Task<AlbumEntity> GetAlbum(int id)
        {
            return _dbContext.Albums.SingleAsync(album => album.Id == id);
        }

        private Task<AlbumTypeEntity> GetAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(albumType => albumType.Name == name);
        }
    }

    private class CommandValidator : AbstractValidator<UpdateAlbumCommand>
    {
        public CommandValidator(IValidator<ImageUpload> imageUploadValidator)
        {
            RuleFor(command => command.Title)
                .MaximumLength(50);

            RuleFor(command => command.Artist)
                .MaximumLength(50);

            RuleFor(command => command.AlbumArt)
                .SetValidator(imageUploadValidator!)
                .When(command => command.AlbumArt != null);
        }
    }
}
