using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve an author by their unique identifier.
    /// </summary>
    /// <remarks>This query is used to fetch detailed information about a specific author, identified by their
    /// <paramref name="AuthorId"/>. Ensure that the provided <paramref name="AuthorId"/> is valid and corresponds to an
    /// existing author in the system.</remarks>
    /// <param name="AuthorId">The unique identifier of the author to retrieve. Must not be <see langword="default"/>.</param>
    public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<BookAuthorDTO>;
}
