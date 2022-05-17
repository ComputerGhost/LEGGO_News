using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Internal.Models
{
    internal class Tag
    {
        public Tag()
        {
            Articles = new HashSet<Article>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        // Relationships

        public virtual ICollection<Article> Articles { get; set; }
    }
}
