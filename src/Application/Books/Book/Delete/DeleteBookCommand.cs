using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to delete a book.
    /// </summary>
    /// <param name="BookId">The unique identifier of the book to be deleted.</param>
    public sealed record DeleteBookCommand(Guid BookId) : ICommand;
}
