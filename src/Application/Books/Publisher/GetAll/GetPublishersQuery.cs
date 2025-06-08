using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a list of book publishers based on the specified search parameters.
    /// </summary>
    /// <remarks>This query is used to filter and retrieve publishers according to the criteria defined in the
    /// <see cref="SearchParamsDTO"/>. The result is a list of publishers matching the search parameters.</remarks>
    /// <param name="SearchParams">The search parameters used to filter the publishers. Cannot be null.</param>
    public sealed record GetPublishersQuery(SearchParamsDTO SearchParams) : IQuery<List<BookPublisherDTO>>;
}
