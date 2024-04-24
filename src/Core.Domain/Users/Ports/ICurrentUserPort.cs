using Core.Domain.Users.Enums;

namespace Core.Domain.Users.Ports;
public interface ICurrentUserPort
{
    string DisplayName { get; }

    bool IsInRole(UserRole role);
}
