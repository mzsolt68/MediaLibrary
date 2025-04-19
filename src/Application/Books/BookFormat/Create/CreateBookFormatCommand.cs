using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record CreateBookFormatCommand(string BookFormatName) : ICommand<Guid>;
}
