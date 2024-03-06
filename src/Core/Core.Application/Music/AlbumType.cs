namespace Core.Application.Music;
public enum AlbumType
{
    Album,
    Collaboration,
    OST,
    Single,
}

internal static class AlbumTypeExtensions
{
    public static Domain.Music.AlbumType ToDomainType(this AlbumType type)
    {
        return Enum.Parse<Domain.Music.AlbumType>(type.ToString());
    }
}
