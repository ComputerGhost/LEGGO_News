using APIClient.DTOs;

namespace APIClient.Connections.Http
{
    public class CharactersConnection : Connection<CharacterDetails, CharacterSaveData, CharacterSummary>, ICharactersConnection
    {
        public CharactersConnection(HttpClient httpClient) :
            base(httpClient, new Uri("characters/", UriKind.Relative))
        {
        }
    }
}
