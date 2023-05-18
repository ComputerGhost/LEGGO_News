using APIClient.Connections;
using APIClient.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class CalendarsController : Controller
    {
        private ICalendarsConnection _calendarsConnection;

        public CalendarsController(ICalendarsConnection calendarsConnection)
        {
            _calendarsConnection = calendarsConnection;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var query = new SearchParameters();
            var results = await _calendarsConnection.ListAsync(query);
            return View(results);
        }
    }
}
