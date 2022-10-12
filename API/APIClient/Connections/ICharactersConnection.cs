using APIClient.DTOs;

namespace APIClient.Connections
{
    public interface ICharactersConnection : IConnection<CharacterDetails, CharacterSaveData, CharacterSummary>
    {
    }
}
