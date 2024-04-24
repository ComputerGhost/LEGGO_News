using Core.Domain.Startup;
using Core.Domain.Users.Enums;
using Core.Domain.Users.Ports;

namespace Core.Infrastructure.Adapters;

[ServiceImplementation]
internal class CurrentUserAdapter : ICurrentUserPort
{
    public string DisplayName => "Developer";

    public bool IsInRole(UserRole role)
    {
        return true;
    }
}
