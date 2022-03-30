using AutoMapper;
using Business.DTOs;
using Business.Repositories.Interfaces;
using Data;
using Data.Models;
using System.Linq;

namespace Business.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public CharacterRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public CharacterSummary Create(CharacterSaveData saveData)
        {
            var newCharacter = _mapper.Map<Character>(saveData);
            _databaseContext.Characters.Add(newCharacter);
            _databaseContext.SaveChanges();

            return _mapper.Map<CharacterSummary>(newCharacter);
        }

        public void Delete(long id)
        {
            var character = _databaseContext.Characters.Find(id);
            _databaseContext.Characters.Remove(character);
            _databaseContext.SaveChanges();
        }

        public CharacterDetails Fetch(long id)
        {
            var character = _databaseContext.Characters.Find(id);
            if (character == null)
            {
                return null;
            }
            return _mapper.Map<CharacterDetails>(character);
        }

        public SearchResults<CharacterSummary> Search(SearchParameters parameters)
        {
            var foundCharacters = _databaseContext.Characters.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Query))
            {
                foundCharacters = foundCharacters.Where(character => 
                    character.EnglishName.Contains(parameters.Query) || 
                    character.KoreanName.Contains(parameters.Query));
            }

            var charactersPage = foundCharacters
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return new SearchResults<CharacterSummary>
            {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = charactersPage.Count(),
                TotalCount = foundCharacters.Count(),
                Data = charactersPage.Select(character => _mapper.Map<CharacterSummary>(character))
            };
        }

        public void Update(long id, CharacterSaveData saveData)
        {
            var character = _databaseContext.Characters.Find(id);
            _mapper.Map(saveData, character);
            _databaseContext.SaveChanges();
        }
    }
}
