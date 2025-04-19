using Application.Abstractions.Messaging;

namespace Application.Books.Publisher.Delete
{
    public sealed record DeletePublisherCommand(Guid PublisherId) : ICommand;
}
