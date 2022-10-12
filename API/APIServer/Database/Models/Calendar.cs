using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServer.Database.Models
{
    internal class Calendar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string GoogleId { get; set; }

        /// <summary>
        /// Hex color for the calendar.
        /// </summary>
        public string Color { get; set; }

        public string Name { get; set; }

        public TimeSpan TimezoneOffset { get; set; }
    }
}
