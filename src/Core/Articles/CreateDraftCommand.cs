using MediatR;

namespace Core.Articles;

/// <summary>
/// Creates a draft of an article.
/// </summary>
public class CreateDraftCommand : IRequest<int>
{
    public string Author { get; set; } = null!;
    public string Content { get; set; } = null!;

    // draftId = createDraft() || createDraft(articleId)
    // updateDraft(draftId)
    // checkpoint = saveCheckpoint(draftId)
    // review(checkpoint)
    // publishArticle(checkpoint)

    // ListDrafts()
    // ListCheckpoints()
    // ListArticles()

    internal class Handler : IRequestHandler<CreateDraftCommand, int>
    {
        public Task<int> Handle(CreateDraftCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
