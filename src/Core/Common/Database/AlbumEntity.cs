using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Common.Database;

[Table("Albums")]
internal class AlbumEntity
{
    public int Id { get; set; }
    public int AlbumTypeId { get; set; }
    public int ImageId { get; set; }
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }

    public virtual AlbumTypeEntity AlbumType { get; set; } = null!;
    public virtual ImageEntity Image { get; set; } = null!;
}
