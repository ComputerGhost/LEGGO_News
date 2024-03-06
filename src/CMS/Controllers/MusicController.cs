using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers;
public class MusicController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Edit(int? id = null)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit([FromForm] AlbumViewModel viewModel, int? id = null)
    {
        return View(viewModel);
    }
}
