using Business.DTOs;

namespace Business.Repositories.Interfaces
{
    public interface ITagRepository
    {
        TagSummary Create(TagSaveData saveData);
        void Delete(long id);
        TagDetails Fetch(long id);
        SearchResults<TagSummary> Search(SearchParameters parameters);
        void Update(long id, TagSaveData saveData);
    }
}
