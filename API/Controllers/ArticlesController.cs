using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController
    {
        private readonly ArticlesService _articlesService;
    }
}
