using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to update a book's details.
    /// </summary>
    public sealed record UpdateBookCommand(UpdateBookDTO BookDTO) : ICommand;
}