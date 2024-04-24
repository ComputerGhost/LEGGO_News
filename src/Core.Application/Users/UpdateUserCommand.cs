using Core.Domain.Common.Entities;
using Core.Domain.Users.Ports;
using FluentValidation;
using MediatR;

namespace Core.Application.Users;
public class UpdateUserCommand : IRequest
{
    /// <summary>
    /// Globally unique identifier for the user.
    /// </summary>
    /// <remarks>
    /// In OAuth, this is usually the 'sub' claim value.
    /// </remarks>
    public string Identity { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    // All other user data should be managed by the IdP.

    internal class Handler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUsersDatabasePort _databaseAdapter;

        public Handler(IUsersDatabasePort databaseAdapter)
        {
            _databaseAdapter = databaseAdapter;
        }

        public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return _databaseAdapter.Upsert(new UserEntity
            {
                Identity = request.Identity,
                DisplayName = request.DisplayName,
            });
        }
    }

    internal class Validator : AbstractValidator<UpdateUserCommand>
    {
        // This is the limit in SAML, so we shouldn't have anything longer than this.
        private const int MAX_IDENTITY_LENGTH = 255;

        public Validator()
        {
            RuleFor(x => x.Identity)
                .Length(1, MAX_IDENTITY_LENGTH);

            RuleFor(x => x.DisplayName)
                .Length(1, 20);
        }
    }
}
