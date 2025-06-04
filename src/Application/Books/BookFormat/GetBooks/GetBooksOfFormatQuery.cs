using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a list of books associated with a specific book format.
    /// </summary>
    /// <param name="BookFormatId">The unique identifier of the book format for which the books are to be retrieved.  This value cannot be <see
    /// langword="null"/>.</param>
    public sealed record GetBooksOfFormatQuery(Guid BookFormatId) : IQuery<List<BookDTO>>;
}
