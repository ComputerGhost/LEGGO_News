using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class MediaController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
