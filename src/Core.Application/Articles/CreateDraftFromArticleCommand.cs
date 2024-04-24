using MediatR;

namespace Core.Application.Articles;

/// <summary>
/// Creates a draft based on an existing article.  This can be used to make changes to an already-published article.
/// </summary>
public class CreateDraftFromArticleCommand : IRequest<int>
{
    public int ArticleId { get; set; }

    internal class Handler : IRequestHandler<CreateDraftFromArticleCommand, int>
    {
        public Task<int> Handle(CreateDraftFromArticleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
