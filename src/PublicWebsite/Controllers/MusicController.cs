using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PublicWebsite.Controllers;

public class MusicController : Controller
{
    private readonly IMediator _mediator;

    public MusicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //public async Task<IActionResult> Index()
    //{
    //    var albums = await _mediator.Send(new GetAlbumsQuery());
    //    return View(albums);
    //}

    //public async Task<IActionResult> Albums()
    //{
    //    var albums = await _mediator.Send(new GetAlbumsQuery
    //    {
    //        Type = AlbumTypes.Album,
    //    });
    //    return View(albums);
    //}

    //public async Task<IActionResult> Collaborations()
    //{
    //    var albums = await _mediator.Send(new GetAlbumsQuery
    //    {
    //        Type = AlbumTypes.Collaboration,
    //    });
    //    return View(albums);
    //}

    //public async Task<IActionResult> OSTs()
    //{
    //    var albums = await _mediator.Send(new GetAlbumsQuery
    //    {
    //        Type = AlbumTypes.OST,
    //    });
    //    return View(albums);
    //}

    //public async Task<IActionResult> Singles()
    //{
    //    var albums = await _mediator.Send(new GetAlbumsQuery
    //    {
    //        Type = AlbumTypes.Single,
    //    });
    //    return View(albums);
    //}
}
