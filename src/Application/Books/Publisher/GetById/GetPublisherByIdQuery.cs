using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a publisher by its unique identifier.
    /// </summary>
    /// <remarks>This query is used to fetch detailed information about a publisher, identified by its <see
    /// cref="PublisherId"/>. The result of the query is a <see cref="BookPublisherDTO"/> object containing the
    /// publisher's data.</remarks>
    /// <param name="PublisherId">The unique identifier of the publisher to retrieve. This value must not be <see langword="default"/>.</param>
    public sealed record GetPublisherByIdQuery(Guid PublisherId) : IQuery<BookPublisherDTO>;
}
