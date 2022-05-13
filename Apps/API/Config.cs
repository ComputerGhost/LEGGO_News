using Calendar.DTOs;

namespace API
{
    public struct Config
    {
        public struct OAuth2Config
        {
            public string Authority { get; set; }
        }

        public CalendarConfig Calendar { get; set; }
        public OAuth2Config OAuth2 { get; set; }
    }
}
