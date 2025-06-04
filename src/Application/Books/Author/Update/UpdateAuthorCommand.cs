using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to update an author's details.
    /// </summary>
    /// <param name="AuthorId">The unique identifier of the author to update.</param>
    /// <param name="FirstName">The updated first name of the author.</param>
    /// <param name="LastName">The updated last name of the author.</param>
    /// <param name="MiddleName">The updated middle name of the author.</param>
    public sealed record UpdateAuthorCommand(Guid AuthorId, string FirstName, string LastName, string MiddleName) : ICommand;
}
