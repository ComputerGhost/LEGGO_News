using Core.Common.Database;
using Core.Startup;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Users;
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

    internal interface IDatabasePort
    {
        Task Upsert(UserEntity userEntity);
    }

    [ServiceImplementation]
    private class DatabaseAdapter : IDatabasePort
    {
        private readonly MyDbContext _dbContext;

        public DatabaseAdapter(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Upsert(UserEntity userEntity)
        {
            return _dbContext.Users.Upsert(userEntity).RunAsync();
        }
    }

    internal class Handler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IDatabasePort _databaseAdapter;

        public Handler(IDatabasePort databaseAdapter)
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
        public Validator()
        {
            RuleFor(x => x.Identity)
                .Length(1, 255);

            RuleFor(x => x.DisplayName)
                .Length(1, 50);
        }
    }
}
