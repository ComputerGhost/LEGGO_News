using System.ComponentModel.DataAnnotations;

namespace APIClient.DTOs
{
    public class TagSaveData
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
