using Core.Startup;

namespace Core.Common.Security;

[ServiceImplementation]
internal class CurrentUserAdapter : ICurrentUserPort
{
    public string DisplayName => "Developer";

    public bool IsInRole(UserRole role)
    {
        return true;
    }
}
