using Core.Application.Common.Exceptions;
using Core.Application.Images;
using Core.Domain.Imaging.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CMS.Controllers;
public class DownloadsController : Controller
{
    private IMediator _mediator;

    public DownloadsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Images(int id, [FromQuery] string size)
    {
        if (!Enum.TryParse<ImageWidth>(size, out var width))
        {
            return NotFound();
        }

        try
        {
            var image = await _mediator.Send(new GetImageQuery(id, width));
            return InlineFile(image.Stream, image.FileName, image.MimeType);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    private FileStreamResult InlineFile(Stream stream, string fileName, string mimeType)
    {
        Response.Headers.ContentDisposition = new ContentDisposition
        {
            FileName = fileName,
            Inline = true,
        }.ToString();
        return File(stream, mimeType);
    }
}
