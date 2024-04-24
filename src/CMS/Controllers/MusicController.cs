using CMS.ViewModels;
using Core.Application.Common.Exceptions;
using Core.Application.Music;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers;
public class MusicController : Controller
{
    private readonly IMediator _mediator;

    public MusicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var albums = await _mediator.Send(new ListAlbumsQuery());
        return View(new MusicIndexViewModel(albums));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(new DeleteAlbumCommand(id));
            return Ok();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new MusicEditViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] MusicEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await _mediator.Send(viewModel.ToCreateAlbumCommand());
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException ex)
        {
            viewModel.AddValidationErrors(ModelState, ex);
            return View(viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var albumDto = await _mediator.Send(new GetAlbumQuery(id));
            return View(new MusicEditViewModel(Url, albumDto));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] MusicEditViewModel viewModel, int id)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await _mediator.Send(viewModel.ToUpdateAlbumCommand(id));
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException ex)
        {
            viewModel.AddValidationErrors(ModelState, ex);
            return View(viewModel);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
