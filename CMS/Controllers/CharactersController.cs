using AutoMapper;
using CMS.ViewModels;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class CharactersController : Controller
    {
        private DatabaseContext DatabaseContext { get; }
        private IMapper Mapper { get; }

        public CharactersController(DatabaseContext databaseContext, IMapper mapper)
        {
            DatabaseContext = databaseContext;
            Mapper = mapper;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CharacterEditViewModel viewModel)
        {
            var character = Mapper.Map<Character>(viewModel);

            DatabaseContext.Add(character);
            DatabaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(long id)
        {
            var character = DatabaseContext.Character.Find(id);
            if (character == null)
                return NotFound();

            DatabaseContext.Remove(character);
            DatabaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(long id)
        {
            var character = DatabaseContext.Character.Find(id);
            if (character == null)
                return NotFound();

            var viewModel = Mapper.Map<CharacterEditViewModel>(character);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(long id, CharacterEditViewModel viewModel)
        {
            var character = DatabaseContext.Character.Find(id);
            if (character == null)
                return NotFound();

            Mapper.Map(viewModel, character);

            DatabaseContext.Update(character);
            DatabaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var characters = DatabaseContext.Character
                .OrderBy(c => c.EnglishName);

            var viewModel = new IndexViewModel<CharacterIndexItemViewModel> {
                Items = characters.Select(c => Mapper.Map<CharacterIndexItemViewModel>(c))
            };

            return View(viewModel);
        }
    }
}
