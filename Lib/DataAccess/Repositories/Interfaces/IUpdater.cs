namespace DataAccess.Repositories.Interfaces
{
    public interface IUpdater<T> : IRepository
    {
        string UpdateSql { get; }
    }
}
