using APIClient.DTOs;

namespace APIServer.Database.Repositories.Interfaces
{
    public interface IMediaRepository
    {
        MediaSummary Create(MediaSaveNewData saveData);
        void Delete(long id);
        MediaDetails Fetch(long id);
        SearchResults<MediaSummary> Search(SearchParameters parameters);
        void Update(long id, MediaSaveExistingData saveData);
    }
}
