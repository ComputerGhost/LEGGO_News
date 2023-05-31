using APIClient.DTOs;

namespace APIClient.Connections
{
    public interface ITagsConnection : IConnection<TagDetails, TagSaveData, TagSummary>
    {
    }
}
