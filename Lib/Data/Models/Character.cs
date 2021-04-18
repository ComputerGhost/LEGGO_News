using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }

        public string KoreanName { get; set; }

        public char Emoji { get; set; }
    }
}
