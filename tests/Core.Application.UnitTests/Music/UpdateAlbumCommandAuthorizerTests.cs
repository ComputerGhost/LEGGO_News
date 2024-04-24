using Core.Application.Music;
using Core.Domain.Users.Enums;

namespace Core.Application.UnitTests.Music;
internal class UpdateAlbumCommandAuthorizerTests : AuthorizerTestsBase
{
    private UpdateAlbumCommand.Authorizer _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _subject = new();
    }

    [TestMethod]
    public void RequiresEditorUserRole()
    {
        // Arrange
        var command = new UpdateAlbumCommand();

        // Act
        _subject.BuildPolicy(command);

        // Assert
        Assert.IsTrue(HasRoleRequirement(_subject.Requirements, UserRole.Editor));
    }

    public static IEnumerable<UserRole[]> UserRolesExceptEditorDataSource =
        Enum.GetValues<UserRole>().Except([UserRole.Editor])
        .Select(x => new[] { x });

    [DataTestMethod]
    [DynamicData(nameof(UserRolesExceptEditorDataSource))]
    public void DoesNotRequireNonEditorRoles(UserRole role)
    {
        // Arrange
        var command = new UpdateAlbumCommand();

        // Act
        _subject.BuildPolicy(command);

        // Assert
        Assert.IsFalse(HasRoleRequirement(_subject.Requirements, role));
    }
}
