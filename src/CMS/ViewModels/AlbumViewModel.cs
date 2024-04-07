﻿using Core.Images;
using Core.Music;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using FluentValidationException = FluentValidation.ValidationException;

namespace CMS.ViewModels;
public class AlbumViewModel
{
    public AlbumViewModel() { }

    public AlbumViewModel(GetAlbumQuery.ResponseDto albumDto)
    {
        AlbumType = albumDto.AlbumType.ToString();
        Title = albumDto.Title;
        Artist = albumDto.Artist;
        ReleaseDate = albumDto.ReleaseDate;
        AlbumArtExistingImageUri = albumDto.AlbumArtUri;
    }

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
    public Uri? AlbumArtExistingImageUri { get; set; }

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
