using Core.Application.Common.Exceptions;
using Core.Application.Common.Requirements;
using Core.Domain.Music.Ports;
using Core.Domain.Users.Enums;
using MediatR;
using MediatR.Behaviors.Authorization;

namespace Core.Application.Music;

/// <summary>
/// Deletes an album from the database.
/// </summary>
/// <remarks>
/// Associated album art is not deleted by this command.
/// </remarks>
public class DeleteAlbumCommand : IRequest
{
    public DeleteAlbumCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    internal class Authorizer : AbstractRequestAuthorizer<DeleteAlbumCommand>
    {
        public override void BuildPolicy(DeleteAlbumCommand request)
        {
            UseRequirement(new MustHaveRoleRequirement(UserRole.Editor));
        }
    }

    internal class Handler : IRequestHandler<DeleteAlbumCommand>
    {
        private readonly IMusicDatabasePort _databaseAdapter;

        public Handler(IMusicDatabasePort databaseAdapter)
        {
            _databaseAdapter = databaseAdapter;
        }

        public async Task Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            var albumEntity = await _databaseAdapter.FetchAlbum(request.Id);
            if (albumEntity == null)
            {
                throw new NotFoundException();
            }

            await _databaseAdapter.DeleteAlbum(request.Id);
        }
    }
}
