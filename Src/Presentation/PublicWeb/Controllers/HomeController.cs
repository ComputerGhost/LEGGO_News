using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicWeb.Models;

namespace PublicWeb.Controllers;

public class HomeController : Controller
{
    private readonly ISender _sender;

    public HomeController(ISender sender)
    {
        _sender = sender;
    }

    public async Task<IActionResult> Index(
        [FromQuery] IndexViewModel viewModel,
        [FromQuery] int? p)
    {
        if (p.HasValue)
        {
            throw new NotImplementedException("This will be the old permalink redirect.");
        }

        await viewModel.Update(_sender);
        return View(viewModel);
    }

    [HttpGet("{newId}/{friendly}")]
    public IActionResult Article(string newId, string? friendly)
    {
        return View("Article");
    }
}
