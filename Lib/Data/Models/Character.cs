using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime? Birthdate { get; set; }

        public string EnglishName { get; set; }

        public string KoreanName { get; set; }

        public string Emoji { get; set; }

        public string Description { get; set; }

        public string Instagram { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
