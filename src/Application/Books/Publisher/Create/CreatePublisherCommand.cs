using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to create a new publisher.
    /// </summary>
    /// <param name="PublisherName">The name of the publisher to be created.</param>
    /// <returns>A unique identifier (<see cref="Guid"/>) for the created publisher.</returns>
    public sealed record CreatePublisherCommand(string PublisherName) : ICommand<Guid>;
}
