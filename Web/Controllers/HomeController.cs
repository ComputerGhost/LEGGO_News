using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Business.Repositories.Interfaces;
using System.Threading.Tasks;
using Web.Models;
using Business.DTOs;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;

        public HomeController(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public IActionResult AboutExid()
        {
            return View();
        }

        public IActionResult Index()
        {
            var parameters = new SearchParameters();
            var articles = _articlesRepository.Search(parameters);
            return View(articles);
        }

        public IActionResult Numbers()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
