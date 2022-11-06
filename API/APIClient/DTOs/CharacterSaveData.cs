using System.ComponentModel.DataAnnotations;

namespace APIClient.DTOs
{
    public class CharacterSaveData
    {
        public DateTime? Birthdate { get; set; }

        [Required]
        public string EnglishName { get; set; } = string.Empty;

        public string? KoreanName { get; set; }

        public string? Emoji { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
