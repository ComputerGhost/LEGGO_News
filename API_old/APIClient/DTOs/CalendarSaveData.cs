using System.ComponentModel.DataAnnotations;

namespace APIClient.DTOs
{
    public class CalendarSaveData
    {
        [Required]
        public string GoogleId { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public TimeSpan TimezoneOffset { get; set; }
    }
}
