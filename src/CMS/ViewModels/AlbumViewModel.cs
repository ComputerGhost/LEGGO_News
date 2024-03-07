using System.ComponentModel.DataAnnotations;

namespace CMS.ViewModels;
public class AlbumViewModel
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Artist { get; set; } = string.Empty;

    [Required]
    public string AlbumType { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    [Required]
    public DateOnly? ReleaseDate { get; set; }

    [DataType(DataType.Upload)]
    [Display(Name = "Album Art")]
    public IFormFile? AlbumArtFile { get; set; }

    public string? AlbumArtUri { get; set; }
}
