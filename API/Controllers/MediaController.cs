using Business.DTOs;
using Business.Repositories.Interfaces;
using Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MediaController : Controller
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MediaSummary))]
        public async Task<IActionResult> Create([FromForm] IFormFile file)
        {
            // TODO: pull most of this into some sort of service class

            var basePath = Path.Combine(Environment.GetEnvironmentVariable("STATIC_BASE_PATH"), "Images");

            var mediaSaveData = new MediaSaveNewData
            {
                OriginalFilename = file.FileName,
            };

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var image = await Media.ImageOperations.Load(memoryStream))
                {
                    image.FileName = $"{Guid.NewGuid()}.{image.Extension}";
                    mediaSaveData.MimeType = image.MimeType;
                    mediaSaveData.LocalFilename = image.FileName;
                    mediaSaveData.LargestResize = await Media.ImageOperations.SaveVariants(image, basePath);
                }
            }

            var summary = _mediaRepository.Create(mediaSaveData);
            return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, [FromBody] MediaSaveExistingData mediaSaveData)
        {
            _mediaRepository.Update(id, mediaSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MediaDetails))]
        public IActionResult Get(int id)
        {
            var media = _mediaRepository.Fetch(id);
            if (media == null)
            {
                return NotFound();
            }
            return Json(media);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchResults<MediaSummary>))]
        public IActionResult List([FromQuery] SearchParameters parameters)
        {
            var searchResults = _mediaRepository.Search(parameters);
            return Json(searchResults);
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

    }
}
