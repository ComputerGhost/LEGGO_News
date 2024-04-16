using MediatR;
using static Core.Articles.ListDraftsQuery;

namespace Core.Articles;
public class ListDraftsQuery : IRequest<ResponseDto>
{
    // User id

    public class ResponseDto
    {
    }
}
