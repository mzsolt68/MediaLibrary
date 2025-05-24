using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve the details of books written by a specific author.
    /// </summary>
    /// <param name="AuthorId">The unique identifier of the author whose books are to be retrieved. This value cannot be empty.</param>
    public sealed record GetBooksOfAuthorQuery(Guid AuthorId) : IQuery<BookAuthorDetailsDTO>;
}
