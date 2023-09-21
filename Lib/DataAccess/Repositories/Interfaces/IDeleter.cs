namespace DataAccess.Repositories.Interfaces
{
    public interface IDeleter : IRepository
    {
        string DeleteSql { get; }
    }
}
