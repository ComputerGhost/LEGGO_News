using MediatR;

namespace Core.Application.Articles;

/// <summary>
/// Publishes an article from an existing checkpoint.  Returns the article id.
/// </summary>
public class PublishArticleCommand : IRequest<int>
{
    public int CheckpointId { get; set; }

    internal class Handler : IRequestHandler<PublishArticleCommand, int>
    {
        public Task<int> Handle(PublishArticleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
