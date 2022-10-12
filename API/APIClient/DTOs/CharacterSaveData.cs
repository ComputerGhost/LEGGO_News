using System;

namespace APIClient.DTOs
{
    public class CharacterSaveData
    {
        public DateTime? Birthdate { get; set; }
        public string EnglishName { get; set; }
        public string KoreanName { get; set; }
        public string Emoji { get; set; }
        public string Description { get; set; }
    }
}
