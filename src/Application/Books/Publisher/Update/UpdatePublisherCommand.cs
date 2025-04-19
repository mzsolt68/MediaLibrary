using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record UpdatePublisherCommand(Guid PublisherId, string PublisherName) : ICommand;
}
