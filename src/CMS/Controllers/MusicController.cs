using CMS.ViewModels;
using Core.Music;
using FluentValidation;
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

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return View(new AlbumViewModel());
        }
        else
        {
            var albumDto = await _mediator.Send(new GetAlbumQuery(id.Value));
            return View(new AlbumViewModel(albumDto));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] AlbumViewModel viewModel, int? id)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            if (id == null)
            {
                await _mediator.Send(viewModel.ToCreateAlbumCommand());
            }
            else
            {
                await _mediator.Send(viewModel.ToUpdateAlbumCommand(id.Value));
            }

            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException ex)
        {
            viewModel.AddValidationErrors(ModelState, ex);
            return View(viewModel);
        }
    }
}
