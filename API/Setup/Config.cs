using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Setup
{
    public class Config
    {
        public string ConnectionString { get; set; } = null!;

        public JwtBearerOptions JwtSettings { get; set; } = null!;
    }
}
