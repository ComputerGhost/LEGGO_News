using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult ContentPolicy()
        {
            return View();
        }

        public IActionResult CookiePolicy()
        {
            return View();
        }

        public IActionResult FairUseNotice()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
