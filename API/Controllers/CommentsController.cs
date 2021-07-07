using API.DTOs;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        public readonly DatabaseContext _context;
        public readonly IMapper _mapper;

        public CommentsController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<CommentSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var foundComments = _context.Comment;

            var commentsPage = foundComments
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<CommentSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = commentsPage.Count(),
                TotalCount = foundComments.Count(),
                Data = commentsPage.Select(comment => _mapper.Map<CommentSummary>(comment)),
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDetails))]
        public IActionResult Get(int id)
        {
            var comment = _context.Comment.Find(id);
            var commentDetails = _mapper.Map<CommentDetails>(comment);
            return Ok(commentDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommentDetails))]
        public IActionResult Create([FromBody] CommentSaveData commentSaveData)
        {
            var comment = _mapper.Map<Comment>(commentSaveData);
            _context.Comment.Add(comment);
            _context.SaveChanges();

            var commentDetails = _mapper.Map<CommentDetails>(comment);
            return CreatedAtAction(nameof(Get), new { id = 0 }, commentDetails);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] CommentSaveData commentSaveData)
        {
            var comment = _context.Comment.Find(id);
            _mapper.Map(commentSaveData, comment);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            var comment = _context.Comment.Find(id);
            _context.Comment.Remove(comment);
            return NoContent();
        }

    }
}
