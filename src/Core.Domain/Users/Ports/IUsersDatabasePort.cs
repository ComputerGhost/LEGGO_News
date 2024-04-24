using Core.Domain.Common.Entities;

namespace Core.Domain.Users.Ports;
public interface IUsersDatabasePort
{
    Task Upsert(UserEntity userEntity);
}
