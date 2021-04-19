using AutoMapper;
using CMS.ViewModels;
using Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class ArticlesController : Controller
    {
        private DatabaseContext DatabaseContext { get; }
        private IMapper Mapper { get; }

        public ArticlesController(DatabaseContext databaseContext, IMapper mapper)
        {
            DatabaseContext = databaseContext;
            Mapper = mapper;
        }

        public IActionResult Edit()
        {
            var emptyModel = new ArticleEditViewModel {
                CharacterOptions = GetCharacterOptions(),
            };
            return View(emptyModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        private Dictionary<long, string> GetCharacterOptions()
        {
            return DatabaseContext.Character.ToDictionary(
                character => character.Id,
                character => character.Emoji);// + " " + character.EnglishName);
        }

    }
}
