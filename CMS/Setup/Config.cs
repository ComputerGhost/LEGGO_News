using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CMS.Setup
{
    public class Config
    {
        public Uri ApiBaseUri { get; set; } = null!;

        public JwtBearerOptions JwtSettings { get; set; } = null!;
    }
}
