namespace DataAccess.Repositories.Interfaces
{
    public interface IFetcher<T> : IRepository
    {
        string FetchSql { get; }
    }
}
