using API.DTOs;
using Business.DTOs;
using Data;
using Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
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
            var foundMedia = _context.Media;

            var mediaPage = foundMedia
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return Json(new SearchResults<MediaSummary> {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = mediaPage.Count(),
                TotalCount = foundMedia.Count(),
                Data = mediaPage.Select(media => new MediaSummary {
                    Id = media.Id,
                    Credit = media.Credit,
                    CreditUrl = media.CreditUrl,
                    OriginalUrl = getImageUrl(media, MediaSize.Original),
                    ThumbnailUrl = getImageUrl(media, MediaSize.Thumbnail),
                    SmallSizeUrl = getImageUrl(media, MediaSize.Small),
                    MediumSizeUrl = getImageUrl(media, MediaSize.Medium),
                    LargeSizeUrl = getImageUrl(media, MediaSize.Large),
                }),
            });
        }

        private static string getImageUrl(Data.Models.Media media, string type)
        {
            var baseUrl = Path.Combine(Environment.GetEnvironmentVariable("STATIC_BASE_URL"), "Images");

            if (type == MediaSize.Original)
                return Path.Combine(baseUrl, media.LocalFilename);

            if (MediaSize.Compare(media.LargestResize, type) >= 0)
                return Path.Combine(baseUrl, type, media.LocalFilename);

            return null;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MediaSummary))]
        public async Task<IActionResult> Create([FromForm] IFormFile file)
        {
            var baseUrl = Path.Combine(Environment.GetEnvironmentVariable("STATIC_BASE_URL"), "Images");
            var basePath = Path.Combine(Environment.GetEnvironmentVariable("STATIC_BASE_PATH"), "Images");

            var newMedia = new Data.Models.Media {
                OriginalFilename = file.FileName,
                Caption = file.FileName,
            };

            using (var memoryStream = new MemoryStream()) {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var image = await Media.ImageOperations.Load(memoryStream)) {
                    image.FileName = $"{Guid.NewGuid()}.{image.Extension}";
                    newMedia.MimeType = image.MimeType;
                    newMedia.LocalFilename = image.FileName;
                    newMedia.LargestResize = await Media.ImageOperations.SaveVariants(image, basePath);
                }
            }

            _context.Media.Add(newMedia);
            _context.SaveChanges();

            return Ok(new MediaSummary {
                Id = newMedia.Id,
                Caption = "Image number " + 0,
                Credit = "Nathan",
                CreditUrl = "https://leggonews.com",
                OriginalUrl = Path.Combine(baseUrl, newMedia.LocalFilename),
                ThumbnailUrl = Path.Combine(baseUrl, "thumbnail", newMedia.LocalFilename),
                SmallSizeUrl = Path.Combine(baseUrl, "small", newMedia.LocalFilename),
                MediumSizeUrl = Path.Combine(baseUrl, "medium", newMedia.LocalFilename),
                LargeSizeUrl = Path.Combine(baseUrl, "large", newMedia.LocalFilename),
            });
        }
    }
}
