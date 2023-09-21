namespace DataAccess.Models
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;

        public string? NextCursor { get; set; }
    }
}
