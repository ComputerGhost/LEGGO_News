using APIClient.Connections;
using APIClient.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class CharactersController : Controller
    {
        private ICharactersConnection _charactersConnection;

        public CharactersController(ICharactersConnection charactersConnection)
        {
            _charactersConnection = charactersConnection;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var query = new SearchParameters();
            var results = await _charactersConnection.ListAsync(query);
            return View(results);
        }
    }
}
