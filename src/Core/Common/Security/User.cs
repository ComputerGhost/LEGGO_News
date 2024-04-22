namespace Core.Common.Security;
public class User
{
    public string Subject { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public IEnumerable<UserRole> Roles { get; set; } = null!;
}
