using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ArticleSaveData
    {
        public string Title { get; set; }
        public string EditorVersion { get; set; }
        public string Content { get; set; }
    }
}
