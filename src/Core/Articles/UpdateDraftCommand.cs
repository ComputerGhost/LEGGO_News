using MediatR;

namespace Core.Articles;

public class UpdateDraftCommand : IRequest
{
    public int DraftId { get; set; }

    internal class Handler : IRequestHandler<UpdateDraftCommand>
    {
        public Task Handle(UpdateDraftCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
