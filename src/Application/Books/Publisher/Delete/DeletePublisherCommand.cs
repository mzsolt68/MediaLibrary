using Application.Abstractions.Messaging;

namespace Application.Books.Publisher.Delete
{
    /// <summary>
    /// Represents a command to delete a publisher.
    /// </summary>
    /// <param name="PublisherId">The unique identifier of the publisher to be deleted.</param>
    public sealed record DeletePublisherCommand(Guid PublisherId) : ICommand;
}
