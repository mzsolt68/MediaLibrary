using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a book by its unique identifier.
    /// </summary>
    /// <param name="BookId">The unique identifier of the book to retrieve. Must not be <see langword="default"/>.</param>
    public sealed record GetBookByIdQuery(Guid BookId) : IQuery<BookDTO>;
}
