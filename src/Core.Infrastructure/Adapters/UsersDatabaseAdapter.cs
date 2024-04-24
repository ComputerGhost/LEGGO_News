using Core.Domain.Common.Entities;
using Core.Domain.Startup;
using Core.Domain.Users.Ports;
using Core.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Adapters;

[ServiceImplementation]
internal class UsersDatabaseAdapter : IUsersDatabasePort
{
    private readonly MyDbContext _dbContext;

    public UsersDatabaseAdapter(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Upsert(UserEntity userEntity)
    {
        return _dbContext.Users.Upsert(userEntity).RunAsync();
    }
}
