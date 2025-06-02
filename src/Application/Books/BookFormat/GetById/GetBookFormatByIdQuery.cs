using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a book format by its unique identifier.
    /// </summary>
    /// <param name="BookFormatId">The unique identifier of the book format to retrieve. Must not be <see langword="default"/>.</param>
    public sealed record GetBookFormatByIdQuery(Guid BookFormatId) : IQuery<BookFormatDTO>;
}
