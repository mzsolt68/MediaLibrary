using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record DeleteBookFormatCommand(Guid BookFormatId) : ICommand;
}
