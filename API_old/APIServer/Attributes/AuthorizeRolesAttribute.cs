using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace APIServer.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles
                .Union(new[] { APIClient.Constants.Roles.Administrator }));
        }
    }
}
