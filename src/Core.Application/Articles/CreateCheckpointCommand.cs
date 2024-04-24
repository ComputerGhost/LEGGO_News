using MediatR;

namespace Core.Application.Articles;

/// <summary>
/// Saves current state of a draft as a checkpoint to be reviewed.  Returns the checkpoint id.
/// </summary>
public class CreateCheckpointCommand : IRequest<int>
{
    public int DraftId { get; set; }

    internal class Handler : IRequestHandler<CreateCheckpointCommand, int>
    {
        public Task<int> Handle(CreateCheckpointCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
