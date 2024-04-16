using MediatR;
using static Core.Articles.ListArticlesQuery;

namespace Core.Articles;
public class ListArticlesQuery : IRequest<ResponseDto>
{
    public class ResponseDto
    {
    }
}
