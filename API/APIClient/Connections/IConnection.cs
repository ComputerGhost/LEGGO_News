using APIClient.DTOs;

namespace APIClient.Connections
{
    public interface IConnection<Details, SaveData, Summary>
    {
        Task<Details> CreateAsync(SaveData data);

        Task DeleteAsync(long id);

        Task<Details> FetchAsync(long id);

        Task<SearchResults<Summary>> ListAsync(SearchParameters query);

        Task UpdateAsync(long id, SaveData data);
    }
}
