using Database.DTOs;

namespace Database.Repositories.Interfaces
{
    public interface ILeadRepository
    {
        LeadSummary Create(LeadSaveData saveData);
        void Delete(long id);
        LeadDetails Fetch(long id);
        SearchResults<LeadSummary> Search(SearchParameters parameters);
        void Update(long id, LeadSaveData saveData);
    }
}
