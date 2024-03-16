using MediatR;

namespace Core.Music;
public class CreateAlbumCommand : IRequest<int>
{
    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public string AlbumArtFileName { get; set; } = null!;

    public Stream AlbumArtStream { get; set; } = null!;
}
