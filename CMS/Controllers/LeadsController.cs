using AutoMapper;
using CMS.ViewModels;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Controllers
{
    public class LeadsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
