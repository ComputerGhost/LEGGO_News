using Calendar.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Setup
{
    public struct Config
    {
        public CalendarConfig Calendar { get; set; }
        public JwtBearerOptions JwtSettings { get; set; }
    }
}
