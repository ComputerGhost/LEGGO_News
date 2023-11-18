using Microsoft.AspNetCore.Mvc;

namespace DefaultController.Common.DefaultController;

public class DefaultController : Controller
{
    public IActionResult Default(string path)
    {
        var viewPath = $"Views/{path}.cshtml";
        return View(viewPath);
    }
}
