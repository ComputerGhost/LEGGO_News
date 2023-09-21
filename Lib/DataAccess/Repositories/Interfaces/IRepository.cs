using System.Data;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRepository
    {
        IDbConnection Connection { get; }
    }
}
