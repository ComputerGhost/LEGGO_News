namespace APIClient.DTOs
{
    public class CharacterDetails
    {
        public long Id { get; set; }

        public DateTime? Birthdate { get; set; }

        public string EnglishName { get; set; } = string.Empty;

        public string KoreanName { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
