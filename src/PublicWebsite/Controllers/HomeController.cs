using Microsoft.AspNetCore.Mvc;

namespace PublicWebsite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
