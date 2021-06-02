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
    public class TagsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TagsController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<TagSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var foundTags = _context.Tag;

            var tagsPage = foundTags
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<TagSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = tagsPage.Count(),
                TotalCount = foundTags.Count(),
                Data = tagsPage.Select(tag => _mapper.Map<TagSummary>(tag)),
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagDetails))]
        public IActionResult Get(long id)
        {
            var tag = _context.Tag.Find(id);
            var tagDetails = _mapper.Map<TagDetails>(tag);
            return Ok(tagDetails);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TagDetails))]
        public IActionResult Create([FromBody] TagSaveData tagSummary)
        {
            var tag = _mapper.Map<Tag>(tagSummary);
            _context.Tag.Add(tag);
            _context.SaveChanges();

            var tagDetails = _mapper.Map<TagDetails>(tag);
            return CreatedAtAction(nameof(Get), new { id = tag.Id }, tagDetails);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(long id, [FromBody] TagSaveData tagSummary)
        {
            var tag = _context.Tag.Find(id);
            tag.Name = tagSummary.Name;
            tag.Description = tagSummary.Description;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(long id)
        {
            var tag = _context.Tag.Find(id);
            _context.Tag.Remove(tag);
            return NoContent();
        }
    }
}
