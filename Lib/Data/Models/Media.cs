using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Media
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string MimeType { get; set; }

        public string LocalFilename { get; set; }

        public string OriginalFilename { get; set; }

        public string LargestResize { get; set; }

        public string Caption { get; set; }

        public string Credit { get; set; }

        public string CreditUrl { get; set; }
    }
}
