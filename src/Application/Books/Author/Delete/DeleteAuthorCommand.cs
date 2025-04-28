using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to delete an author associated with a specific book.
    /// </summary>
    /// <param name="bookId">The unique identifier of the book associated with the author to be deleted.</param>
    public sealed record DeleteAuthorCommand(Guid bookId) : ICommand
    {
    }
}
