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
    public class TagsController : Controller
    {
        private DatabaseContext DatabaseContext { get; set; }
        private IMapper Mapper { get; }

        public TagsController(DatabaseContext databaseContext, IMapper mapper)
        {
            DatabaseContext = databaseContext;
            Mapper = mapper;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TagEditViewModel viewModel)
        {
            var tag = Mapper.Map<Tag>(viewModel);

            DatabaseContext.Add(tag);
            DatabaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(long id)
        {
            var tag = DatabaseContext.Tag.Find(id);
            if (tag == null)
                return NotFound();

            DatabaseContext.Remove(tag);
            DatabaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(long id)
        {
            var tag = DatabaseContext.Tag.Find(id);
            if (tag == null)
                return NotFound();

            var viewModel = Mapper.Map<TagEditViewModel>(tag);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(long id, TagEditViewModel viewModel)
        {
            var tag = DatabaseContext.Tag.Find(id);
            if (tag == null)
                return NotFound();

            Mapper.Map(viewModel, tag);

            DatabaseContext.Update(tag);
            DatabaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var tags = DatabaseContext.Tag
                .OrderBy(t => t.Name);

            var viewModel = new IndexViewModel<TagIndexItemViewModel> {
                Items = tags.Select(t => Mapper.Map<TagIndexItemViewModel>(t))
            };

            return View(viewModel);
        }
    }
}
