using Core.Downloads.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Downloads.Handlers;

public class FileByNameHandler
{
    public static async Task<IResult> Handle(string filename, [FromServices] IMediator mediator)
    {
        var fileInfo = await mediator.Send(new GetFileByFilenameQuery(filename));
        return Results.Stream(fileInfo.Stream, fileInfo.MimeType, fileInfo.Filename);
    }
}
