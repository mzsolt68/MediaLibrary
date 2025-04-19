using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record CreatePublisherCommand(string PublisherName) : ICommand<Guid>;
}
