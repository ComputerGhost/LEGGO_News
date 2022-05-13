using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.DTOs
{
    public struct EventInfo
    {
        public string GoogleCalendarId { get; set; }

        /// <summary>
        /// This color will match the color of the calendar.
        /// </summary>
        public string Color { get; set; }

        public DateTimeOffset End { get; set; }

        /// <summary>
        /// URL of the event on Google
        /// </summary>
        public string EventUri { get; set; }

        public bool IsAllDay { get; set; }

        public DateTimeOffset Start { get; set; }

        public string Title { get; set; }
    }
}
