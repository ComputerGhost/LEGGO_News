using MediatR;

namespace Core.Application.Articles;

/// <summary>
/// Adds a user review to a checkpoint.  Returns the review id.
/// </summary>
public class ReviewCheckpointCommand : IRequest<int>
{
    public int CheckpointId { get; set; }

    // TODO: Approved, comment, hold, denied
    public int Status { get; set; }

    public string Comment { get; set; } = null!;

    public int CommentPosition { get; set; }

    internal class Handler : IRequestHandler<ReviewCheckpointCommand, int>
    {
        public Task<int> Handle(ReviewCheckpointCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
