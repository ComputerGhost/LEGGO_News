using CMS.ViewModels;
using Core.Application;
using Core.Application.Music;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers;
public class MusicController : Controller
{
    private IMediator _mediator;

    public MusicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Edit(int? id = null)
    {
        var viewModel = new AlbumViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] AlbumViewModel viewModel, int? id = null)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (id == null)
        {
            id = await _mediator.Send(new CreateAlbumCommand
            {
                AlbumType = Enum.Parse<AlbumType>(viewModel.AlbumType),
                Title = viewModel.Title,
                Artist = viewModel.Artist,
                ReleaseDate = viewModel.ReleaseDate!.Value,
                AlbumArtFileName = viewModel.AlbumArtFile!.FileName,
                AlbumArtStream = viewModel.AlbumArtFile!.OpenReadStream(),
            });
        }

        return RedirectToAction("Edit", new { id });
    }
}
