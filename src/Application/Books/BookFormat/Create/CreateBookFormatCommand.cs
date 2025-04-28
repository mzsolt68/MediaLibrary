using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to create a new book format.
    /// </summary>
    /// <param name="BookFormatName">The name of the book format to be created.</param>
    /// <returns>A unique identifier (<see cref="Guid"/>) for the created book format.</returns>
    public sealed record CreateBookFormatCommand(string BookFormatName) : ICommand<Guid>;
}
