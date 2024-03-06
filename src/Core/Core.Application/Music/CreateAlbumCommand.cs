using Core.Domain.FileStorage;
using Core.Domain.FileStorage.Ports;
using Core.Domain.Music;
using Core.Domain.Ports;
using MediatR;

namespace Core.Application;
public class CreateAlbumCommand : IRequest<int>
{
    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public Stream AlbumArtStream { get; set; } = null!;

    public string AlbumArtFileName { get; set; } = null!;

    internal class CommandHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IAlbumsRepository _albumsRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly IFileSystem _fileSystem;
        private readonly IImagesRepository _imagesRepository;

        public CommandHandler(
            IAlbumsRepository albumsRepository,
            IFilesRepository filesRepository, 
            IFileSystem fileSystem,
            IImagesRepository imagesRepository)
        {
            _albumsRepository = albumsRepository;
            _filesRepository = filesRepository;
            _fileSystem = fileSystem;
            _imagesRepository = imagesRepository;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var fileId = await SaveAlbumArt(request.AlbumArtStream, request.AlbumArtFileName);
            return await SaveAlbumData(request.AlbumType, request.Title, request.Artist, request.ReleaseDate, fileId);
        }

        private Task<int> SaveAlbumArt(Stream stream, string fileName)
        {
            var fileWriter = new ImageWriter(_filesRepository, _fileSystem, _imagesRepository);
            return fileWriter.CreateImage(stream, fileName);
        }

        private Task<int> SaveAlbumData(AlbumType albumType, string title, string artist, DateOnly releaseDate, int albumArtFileId)
        {
            var albumWriter = new AlbumWriter(_albumsRepository);
            return albumWriter.CreateAlbum(albumType, albumArtFileId, title, artist, releaseDate);
        }
    }
}
