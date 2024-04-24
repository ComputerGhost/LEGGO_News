using MediatR;
using static Core.Application.Articles.ListArticlesQuery;

namespace Core.Application.Articles;
public class ListArticlesQuery : IRequest<ResponseDto>
{
    public class ResponseDto
    {
    }
}
