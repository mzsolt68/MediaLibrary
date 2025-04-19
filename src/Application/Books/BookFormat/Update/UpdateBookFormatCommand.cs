using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record UpdateBookFormatCommand(Guid BookFormatId, string BookFormatName) : ICommand;
}
