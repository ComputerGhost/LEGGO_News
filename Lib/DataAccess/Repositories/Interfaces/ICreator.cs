namespace DataAccess.Repositories.Interfaces
{
    public interface ICreator<T> : IRepository
    {
        string InsertSql { get; }
    }
}
