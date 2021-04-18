using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CharacterSummary
    {
        public int Id { get; set; }

        public string Emoji { get; set; }

        public string EnglishName { get; set; }
    }
}
