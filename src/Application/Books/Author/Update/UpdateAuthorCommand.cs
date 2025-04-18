using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record UpdateAuthorCommand(Guid AuthorId, string FirstName, string LastName, string MiddleName) : ICommand
    {
    }
}
