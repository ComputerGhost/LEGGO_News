using Microsoft.AspNetCore.Authorization;

namespace CMS.Attributes
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
