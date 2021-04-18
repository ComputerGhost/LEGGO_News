using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CharacterDetails
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }

        public string KoreanName { get; set; }

        public char Emoji { get; set; }
    }
}
