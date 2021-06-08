using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Lead
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? PrimaryDate { get; set; }

        public string PrimaryUrl { get; set; }
    }
}
