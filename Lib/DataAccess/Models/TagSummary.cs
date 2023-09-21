namespace DataAccess.Models
{
    public class TagSummary
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FriendlyUriSegment { get; set; } = string.Empty;
    }
}
