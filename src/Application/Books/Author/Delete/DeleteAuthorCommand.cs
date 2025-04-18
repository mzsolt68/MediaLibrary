using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record DeleteAuthorCommand(Guid bookId) : ICommand
    {
    }
}
