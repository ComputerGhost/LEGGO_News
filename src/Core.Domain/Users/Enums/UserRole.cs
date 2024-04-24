namespace Core.Domain.Users.Enums;
public enum UserRole
{
    /// <summary>
    /// Manages the website's systems.
    /// </summary>
    Administrator,

    /// <summary>
    /// Prepares and schedules articles to be published.
    /// </summary>
    Editor,

    /// <summary>
    /// Creates news articles.
    /// </summary>
    Journalist,

    /// <summary>
    /// Moderates content submitted from non-staff, such as comments.
    /// </summary>
    Moderator,

    /// <summary>
    /// Reviews articles and can suggest edits.
    /// </summary>
    Reviewer,
}
