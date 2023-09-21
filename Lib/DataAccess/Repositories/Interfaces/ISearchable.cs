namespace DataAccess.Repositories.Interfaces
{
    public interface ISearchable<T> : IRepository
    {
        string SearchSql { get; }
        string PointerFunc(T entry);
    }
}
