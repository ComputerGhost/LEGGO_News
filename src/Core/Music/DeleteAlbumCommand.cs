using Core.Common;
using Core.Common.Database;
using MediatR;
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

    private class CommandHandler : IRequestHandler<DeleteAlbumCommand>
    {
        private readonly MyDbContext _dbContext;

        public CommandHandler(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            var albumEntity = await GetAlbum(request.Id);
            if (albumEntity == null)
            {
                throw new NotFoundException();
            }

            await DeleteAlbum(request.Id);
        }

        private Task<AlbumEntity?> GetAlbum(int id)
        {
            return _dbContext.Albums.SingleOrDefaultAsync(album => album.Id == id);
        }

        private Task DeleteAlbum(int id)
        {
            return _dbContext.Albums
                .Where(album => album.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
