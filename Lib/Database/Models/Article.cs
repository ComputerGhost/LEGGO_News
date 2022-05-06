using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Article
    {
        public Article()
        {
            Tags = new HashSet<Tag>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Format { get; set; }

        public string Content { get; set; }


        // Relationships

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
