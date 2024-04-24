using Core.Application.Common.Requirements;
using Core.Domain.Users.Enums;
using MediatR.Behaviors.Authorization;

namespace Core.Application.UnitTests;
public class AuthorizerTestsBase : HandlerTestsBase
{
    protected static IEnumerable<UserRole> AllUserRoles => Enum.GetValues<UserRole>();

    protected bool HasRoleRequirement(IEnumerable<IAuthorizationRequirement> requirements, UserRole role)
    {
        return requirements.Any(x => (x as MustHaveRoleRequirement)?.Role == role);
    }

    protected static IEnumerable<object[]> CreateDataSource<T>(IEnumerable<T> values)
        where T : notnull
    {
        return values.Select(x => new[] { (object)x });
    }
}
