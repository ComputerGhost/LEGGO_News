using Microsoft.AspNetCore.Authorization;

namespace Users.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles
                .Union(new[] { Constants.Roles.Administrator }));
        }
    }
}
