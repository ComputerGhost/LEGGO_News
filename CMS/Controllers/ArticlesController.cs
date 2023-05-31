using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ArticlesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
    }
}
