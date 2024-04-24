using MediatR;
using static Core.Application.Articles.ListDraftsQuery;

namespace Core.Application.Articles;
public class ListDraftsQuery : IRequest<ResponseDto>
{
    // User id

    public class ResponseDto
    {
    }
}
