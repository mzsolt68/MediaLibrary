using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to update a publisher's details.
    /// </summary>
    /// <param name="PublisherId">The unique identifier of the publisher to be updated.</param>
    /// <param name="PublisherName">The new name of the publisher.</param>
    public sealed record UpdatePublisherCommand(Guid PublisherId, string PublisherName) : ICommand;
}
