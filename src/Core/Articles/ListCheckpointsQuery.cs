using MediatR;
using static Core.Articles.ListCheckpointsQuery;

namespace Core.Articles;

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
