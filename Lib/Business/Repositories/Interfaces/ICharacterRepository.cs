using Business.DTOs;

namespace Business.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        CharacterSummary Create(CharacterSaveData saveData);
        void Delete(long id);
        CharacterDetails Fetch(long id);
        SearchResults<CharacterSummary> Search(SearchParameters parameters);
        void Update(long id, CharacterSaveData saveData);
    }
}
