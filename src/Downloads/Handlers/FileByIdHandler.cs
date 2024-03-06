using Core.Downloads.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Downloads.Handlers;

public class FileByIdHandler
{
    public static async Task<IResult> Handle(int fileId, [FromServices] IMediator mediator)
    {
        var fileInfo = await mediator.Send(new GetFileByIdQuery(fileId));
        return Results.Stream(fileInfo.Stream, fileInfo.MimeType, fileInfo.Filename);
    }
}
