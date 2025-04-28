using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to update a book format.
    /// </summary>
    /// <param name="BookFormatId">The unique identifier of the book format to update.</param>
    /// <param name="BookFormatName">The new name of the book format.</param>
    public sealed record UpdateBookFormatCommand(Guid BookFormatId, string BookFormatName) : ICommand;
}
