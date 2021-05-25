using API.DTOs;
using Data;
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
    public class MediaController : Controller
    {
        private readonly DatabaseContext _context;

        public MediaController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchResults<MediaSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var imageSource = new List<MediaSummary>();
            for (int i = 0; i != 200; ++i) {
                imageSource.Add(new MediaSummary {
                    Id = i,
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
            });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MediaSummary))]
        public async Task<IActionResult> Create([FromForm] IFormFile file)
        {
            var basePath = Environment.GetEnvironmentVariable("DOWNLOADS_FOLDER");

            using (var memoryStream = new MemoryStream()) {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var image = await Media.ImageOperations.Load(memoryStream)) {
                    image.FileName = $"{Guid.NewGuid()}.{image.Extension}";
                    await Media.ImageOperations.SaveVariants(image, basePath);
                }
            }

            var newMedia = new Data.Models.Media {
                FileName = file.FileName,
                MimeType = file.ContentType,
            };
            _context.Media.Add(newMedia);
            _context.SaveChanges();

            return Ok(new MediaSummary {
                Id = newMedia.Id,
                Caption = "Image number " + 0,
                Credit = "Nathan",
                CreditUrl = "https://leggonews.com",
                OriginalUrl = Path.Combine(basePath, newMedia.FileName),
                ThumbnailUrl = Path.Combine(basePath, "thumbnail", newMedia.FileName),
                SmallSizeUrl = Path.Combine(basePath, "small", newMedia.FileName),
                MediumSizeUrl = Path.Combine(basePath, "medium", newMedia.FileName),
                LargeSizeUrl = Path.Combine(basePath, "large", newMedia.FileName),
            });
        }
    }
}
