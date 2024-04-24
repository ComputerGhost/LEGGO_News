using Core.Application.Music;
using Core.Domain.Users.Enums;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class DeleteAlbumCommandAuthorizerTests : AuthorizerTestsBase
{
    private DeleteAlbumCommand.Authorizer _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _subject = new();
    }

    [TestMethod]
    public void RequiresEditorUserRole()
    {
        // Arrange
        var command = new DeleteAlbumCommand(INCONSEQUENTIAL_ID);

        // Act
        _subject.BuildPolicy(command);

        // Assert
        Assert.IsTrue(HasRoleRequirement(_subject.Requirements, UserRole.Editor));
    }

    public static IEnumerable<object[]> UserRolesExceptEditorDataSource =>
        CreateDataSource(AllUserRoles.Except([UserRole.Editor]));

    [DataTestMethod]
    [DynamicData(nameof(UserRolesExceptEditorDataSource))]
    public void DoesNotRequireNonEditorRoles(UserRole role)
    {
        // Arrange
        var command = new DeleteAlbumCommand(INCONSEQUENTIAL_ID);

        // Act
        _subject.BuildPolicy(command);

        // Assert
        Assert.IsFalse(HasRoleRequirement(_subject.Requirements, role));
    }
}
