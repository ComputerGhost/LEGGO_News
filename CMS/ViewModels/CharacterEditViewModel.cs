using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class CharacterEditViewModel
    {
        public DateTime? Birthdate { get; set; }

        public string Description { get; set; }

        public string Emoji { get; set; }

        public string EnglishName { get; set; }

        public string KoreanName { get; set; }

        public string Instagram { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
