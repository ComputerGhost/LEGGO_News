using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Common.Database;

[Table("Images")]
internal class ImageEntity
{
    public int Id { get; set; }
    public int OriginalFileId { get; set; }
    public int? LargeFileId { get; set; }
    public int? MediumFileId { get; set; }
    public int ThumbnailFileId { get; set; }

    public virtual FileEntity OriginalFile { get; set; } = null!;
    public virtual FileEntity? LargeFile { get; set; }
    public virtual FileEntity? MediumFile { get; set; }
    public virtual FileEntity ThumbnailFile { get; set; } = null!;
}
