namespace Core.Common.Security;
public interface ICurrentUserPort
{
    string DisplayName { get; }

    bool IsInRole(UserRole role);
}
