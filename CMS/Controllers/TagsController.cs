using CMS.Constants;
using CMS.ViewModels;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private TagsService _tagsService;

        public TagsController(TagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                UserCanDelete = User.IsInRole(Roles.Administrator),
                UserCanEdit = User.IsInRole(Roles.Administrator),
            });
        }
    }
}
