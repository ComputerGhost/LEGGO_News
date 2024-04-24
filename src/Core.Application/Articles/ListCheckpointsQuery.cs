using MediatR;
using static Core.Application.Articles.ListCheckpointsQuery;

namespace Core.Application.Articles;

/// <summary>
/// Lists checkpoints, or draft states ready to be reviewed.
/// </summary>
public class ListCheckpointsQuery : IRequest<ResponseDto>
{
    // User id

    public class ResponseDto
    {
    }
}
