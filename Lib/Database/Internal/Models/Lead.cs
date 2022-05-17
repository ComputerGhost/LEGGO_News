using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Internal.Models
{
    internal class Lead
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? PrimaryDate { get; set; }

        public string PrimaryUrl { get; set; }
    }
}
