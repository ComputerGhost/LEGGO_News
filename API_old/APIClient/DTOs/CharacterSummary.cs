namespace APIClient.DTOs
{
    public class CharacterSummary
    {
        public long Id { get; set; }

        public string EnglishName { get; set; } = string.Empty;

        public string? KoreanName { get; set; }
    }
}
