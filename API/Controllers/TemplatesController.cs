using API.DTOs;
using AutoMapper;
using Data;
using Data.Constants;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplatesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TemplatesController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<TemplateSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var foundTemplates = _context.Template;

            var templatesPage = foundTemplates
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<TemplateSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = templatesPage.Count(),
                TotalCount = foundTemplates.Count(),
                Data = templatesPage.Select(tag => _mapper.Map<TemplateSummary>(tag)),
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemplateDetails))]
        public IActionResult Get(long id)
        {
            var template = _context.Template.Find(id);
            var templateDetails = _mapper.Map<TemplateDetails>(template);
            return Ok(templateDetails);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TemplateDetails))]
        public IActionResult Create([FromBody] TemplateSaveData templateSaveData)
        {
            var template = _mapper.Map<Template>(templateSaveData);
            _context.Template.Add(template);
            _context.SaveChanges();

            var templateDetails = _mapper.Map<TemplateDetails>(template);
            return CreatedAtAction(nameof(Get), new { id = template.Id }, templateDetails);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(long id, [FromBody] TemplateSaveData templateSaveData)
        {
            var template = _context.Template.Find(id);
            _mapper.Map(templateSaveData, template);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(long id)
        {
            var template = _context.Template.Find(id);
            _context.Template.Remove(template);
            return NoContent();
        }
    }
}
