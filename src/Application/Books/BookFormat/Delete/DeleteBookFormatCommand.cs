using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to delete a book format.
    /// </summary>
    /// <param name="BookFormatId">The unique identifier of the book format to be deleted.</param>
    public sealed record DeleteBookFormatCommand(Guid BookFormatId) : ICommand;
}
