using Microsoft.AspNetCore.Mvc;

namespace PublicWebsite.Controllers;

public class DefaultController : Controller
{
    public IActionResult Default(string path)
    {
        var viewPath = $"Views/{path}.cshtml";
        return View(viewPath);
    }
}
