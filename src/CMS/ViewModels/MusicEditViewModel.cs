using CMS.Extensions;
using Core.Common.Imaging;
using Core.Music;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using FluentValidationException = FluentValidation.ValidationException;

namespace CMS.ViewModels;
public class MusicEditViewModel
{
    public MusicEditViewModel()
    {
        // It is a new album.
        AlbumId = null;
        CanDelete = false;
    }

    public MusicEditViewModel(
        IUrlHelper urlHelper, 
        GetAlbumQuery.ResponseDto albumDto)
    {
        // It is an existing album.
        AlbumId = albumDto.Id;
        CanDelete = true;

        AlbumType = albumDto.AlbumType.ToString();
        Title = albumDto.Title;
        Artist = albumDto.Artist;
        ReleaseDate = albumDto.ReleaseDate;
        AlbumArtExistingImageUrl = urlHelper.GetThumbnailUrl(albumDto.AlbumArtImageId);
    }

    public bool CanDelete { get; init; }

    public int? AlbumId { get; init; }

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

    [Display(Name = "Album Art")]
    [DataType(DataType.Upload)]
    public IFormFile? AlbumArtUploadedFile { get; set; }
    public string? AlbumArtExistingImageUrl { get; set; }

    public void AddValidationErrors(ModelStateDictionary modelState, FluentValidationException validationException)
    {
        foreach (var error in validationException.Errors)
        {
            var propertyName = error.PropertyName;
            if (propertyName.StartsWith("AlbumArt."))
            {
                propertyName = nameof(AlbumArtUploadedFile);
            }

            modelState.AddModelError(propertyName, error.ErrorMessage);
        }
    }

    public CreateAlbumCommand ToCreateAlbumCommand()
    {
        return new CreateAlbumCommand
        {
            AlbumType = Enum.Parse<AlbumType>(AlbumType),
            Title = Title,
            Artist = Artist,
            ReleaseDate = ReleaseDate!.Value,
            AlbumArt = new ImageUpload
            {
                FileName = AlbumArtUploadedFile!.FileName,
                Stream = AlbumArtUploadedFile!.OpenReadStream(),
            },
        };
    }

    public UpdateAlbumCommand ToUpdateAlbumCommand(int id)
    {
        return new UpdateAlbumCommand
        {
            Id = id,
            AlbumType = Enum.Parse<AlbumType>(AlbumType),
            Title = Title,
            Artist = Artist,
            ReleaseDate = ReleaseDate!.Value,
            AlbumArt = new ImageUpload
            {
                FileName = AlbumArtUploadedFile!.FileName,
                Stream = AlbumArtUploadedFile!.OpenReadStream(),
            },
        };
    }
}
