using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve detailed information about a specific book.
    /// </summary>
    /// <param name="BookId">The unique identifier of the book to retrieve details for. Must not be an empty <see cref="Guid"/>.</param>
    public sealed record GetBookWithDetailsQuery(Guid BookId) : IQuery<BookDetailsDTO>;
}
