using API.Modules.Articles.DTOs;
using API.Modules.Articles.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Modules.Articles
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IDbConnection _connection;

        public ArticlesController(IDbConnection connection)
        {
            _connection = connection;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        public async Task<IActionResult> Create(CreateArticleDto creationDto)
        {
            var service = new CreateArticleService(_connection);
            var articleId = await service.CreateArticleAsync(creationDto);
            return Ok(articleId);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
        }

        [HttpPut("{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int articleId, 
            UpdateArticleDto editArticleDto)
        {
            var service = new UpdateArticleService(_connection);
            await service.UpdateArticleAsync(articleId, editArticleDto);
            return Ok();
        }
    }
}
