using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Common.Database;

[Table("AlbumTypes")]
internal class AlbumTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
