using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record DeleteBookCommand(Guid BookId) : ICommand
    {
    }
}
