using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to create a new author with the specified name details.
    /// </summary>
    /// <param name="FirstName">The first name of the author. This value cannot be null or empty.</param>
    /// <param name="LastName">The last name of the author. This value cannot be null or empty.</param>
    /// <param name="MiddleName">The middle name of the author. This value can be null or empty if the author does not have a middle name.</param>
    public sealed record CreateAuthorCommand(string FirstName, string LastName, string MiddleName) : ICommand<Guid>;
}
