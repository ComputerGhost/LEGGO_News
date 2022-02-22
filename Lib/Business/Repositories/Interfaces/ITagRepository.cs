using Business.DTOs;

namespace Business.Repositories.Interfaces
{
    public interface ITagRepository
    {
        TagSummary Create(TagSaveData saveData);
        void Update(long id, TagSaveData saveData);
        TagDetails Fetch(long id);
        SearchResults<TagSummary> Search(SearchParameters parameters);
        void Delete(long id);
    }
}
