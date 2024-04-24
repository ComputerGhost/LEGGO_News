namespace Core.Domain.Common.Entities;
public class FileEntity
{
    public int Id { get; set; }

    /// <summary>
    /// The name of the file saved on the server.
    /// </summary>
    public string PrivateFileName { get; set; } = null!;

    /// <summary>
    /// The filename to report to the user.
    /// </summary>
    public string PublicFileName { get; set; } = null!;
}
