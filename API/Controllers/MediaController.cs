using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : Controller
    {

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchResults<MediaSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var imageSource = new List<MediaSummary>();
            for (int i = 0; i != 200; ++i) {
                imageSource.Add(new MediaSummary {
                    ImageId = i,
                    Caption = "Image number " + i,
                    Credit = "Nathan",
                    CreditUrl = "https://leggonews.com",
                    OriginalUrl = "https://leggonews.com/wp-content/uploads/2021/05/channels4_profile.jpg",
                });
            }

            var returnedImages = imageSource.AsQueryable()
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<MediaSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = returnedImages.Count(),
                TotalCount = imageSource.Count(),
                Data = returnedImages
            }); ;
        }
    }
}
