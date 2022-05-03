﻿using Microsoft.AspNetCore.Mvc;
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
    public class AboutController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Exid()
        {
            return View();
        }

        public IActionResult Pledge()
        {
            return View();
        }

        public IActionResult Staff()
        {
            return View();
        }

        public IActionResult Support()
        {
            return View();
        }
    }
}
