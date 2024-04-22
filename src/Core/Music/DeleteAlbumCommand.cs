using Core.Common;
using Core.Common.Database;
using Core.Common.Security;
using Core.Startup;
using MediatR;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Core.Music;

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

    internal interface IDatabasePort
    {
        Task DeleteAlbum(int id);
        Task<AlbumEntity?> FetchAlbum(int id);
    }

    [ServiceImplementation]
    private class DatabaseAdapter : IDatabasePort
    {
        private readonly MyDbContext _dbContext;

        public DatabaseAdapter(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task DeleteAlbum(int id)
        {
            return _dbContext.Albums.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public Task<AlbumEntity?> FetchAlbum(int id)
        {
            return _dbContext.Albums.SingleOrDefaultAsync(x => x.Id == id);
        }
    }

    internal class Authorizer : AbstractRequestAuthorizer<DeleteAlbumCommand>
    {
        public override void BuildPolicy(DeleteAlbumCommand request)
        {
            UseRequirement(new MustHaveRoleRequirement(UserRole.Editor));
        }
    }

    internal class Handler : IRequestHandler<DeleteAlbumCommand>
    {
        private readonly IDatabasePort _databaseAdapter;

        public Handler(IDatabasePort databaseAdapter)
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
