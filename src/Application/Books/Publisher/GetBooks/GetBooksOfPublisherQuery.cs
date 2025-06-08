using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve details about books published by a specific publisher.
    /// </summary>
    /// <remarks>This query is used to fetch information about books associated with a given publisher. The
    /// result includes detailed data about the publisher and their books.</remarks>
    /// <param name="PublisherId">The unique identifier of the publisher whose books are to be retrieved. Must be a valid <see cref="Guid"/>.</param>
    public sealed record GetBooksOfPublisherQuery(Guid PublisherId) : IQuery<BookPublisherDetailsDTO>;
}
