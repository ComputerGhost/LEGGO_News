using MediatR.Behaviors.Authorization;

namespace Core.Common.Security;
internal class MustHaveRoleRequirement : IAuthorizationRequirement
{
    public MustHaveRoleRequirement(UserRole role)
    {
        Role = role;
    }

    public UserRole Role { get; set; }

    internal class Handler : IAuthorizationHandler<MustHaveRoleRequirement>
    {
        private readonly ICurrentUserPort _currentUserAdapter;

        public Handler(ICurrentUserPort currentUserAdapter)
        {
            _currentUserAdapter = currentUserAdapter;
        }

        public Task<AuthorizationResult> Handle(MustHaveRoleRequirement requirement, CancellationToken cancellationToken = default)
        {
            var result = HandleSynchronously(requirement);
            return Task.FromResult(result);
        }

        private AuthorizationResult HandleSynchronously(MustHaveRoleRequirement requirement)
        {
            if (!_currentUserAdapter.IsInRole(requirement.Role))
            {
                var roleName = requirement.Role.ToString();
                return AuthorizationResult.Fail($"The user role {roleName} is required for this action.");
            }

            return AuthorizationResult.Succeed();
        }
    }
}
