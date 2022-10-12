using APIClient.DTOs;

namespace APIClient.Connections.Http
{
    public class TagsConnection : Connection<TagDetails, TagSaveData, TagSummary>, ITagsConnection
    {
        public TagsConnection(HttpClient httpClient) :
            base(httpClient, new Uri("tags/", UriKind.Relative))
        {
        }
    }
}
