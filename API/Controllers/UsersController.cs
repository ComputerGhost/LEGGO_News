using API.DTOs;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UsersController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResults<UserSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var foundUsers = _context.User;

            var usersPage = foundUsers
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<UserSummary>() {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = usersPage.Count(),
                TotalCount = foundUsers.Count(),
                Data = usersPage.Select(user => _mapper.Map<UserSummary>(user)),
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDetails))]
        public IActionResult Get(long id)
        {
            var user = _context.User.Find(id);
            var userDetails = _mapper.Map<UserDetails>(user);
            return Ok(userDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] UserSaveData userSaveData)
        {
            var user = _mapper.Map<User>(userSaveData);
            _context.User.Add(user);
            _context.SaveChanges();

            var userDetails = _mapper.Map<UserDetails>(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, userDetails);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(long id, [FromBody] UserSaveData userSaveData)
        {
            var user = _context.User.Find(id);
            _mapper.Map(userSaveData, user);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
