using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to create a new book.
    /// </summary>
    public sealed record CreateBookCommand(CreateBookDTO BookDTO) : ICommand<Guid>;
}