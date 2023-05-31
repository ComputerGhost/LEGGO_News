using APIClient.Constants;
using APIClient.DTOs;
using APIServer.Attributes;
using APIServer.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace APIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : Controller
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Journalist)]
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
            return CreatedAtAction(nameof(Fetch), new { id = summary.Id }, summary);
        }

        [HttpPut("{id}")]
        [AuthorizeRoles(Roles.Journalist, Roles.Editor)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int id, [FromBody] MediaSaveExistingData mediaSaveData)
        {
            _mediaRepository.Update(id, mediaSaveData);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MediaDetails))]
        public IActionResult Fetch(int id)
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

    }
}
