using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Common.Database;

[Table("Users")]
internal class UserEntity
{
    public string Identity { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
}
