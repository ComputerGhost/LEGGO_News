using Microsoft.AspNetCore.Mvc;

namespace PublicWebsite.Controllers;

public class ScheduleController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
