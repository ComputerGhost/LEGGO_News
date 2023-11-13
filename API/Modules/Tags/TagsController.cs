using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Modules.Tags
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : Controller
    {
        private readonly IDbConnection _connection;

        public TagsController(IDbConnection connection)
        {
            _connection = connection;
        }
    }
}
