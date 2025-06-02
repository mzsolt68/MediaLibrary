using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a list of book formats based on the specified search parameters.
    /// </summary>
    /// <param name="SearchParams">The search parameters used to filter the book formats. This parameter cannot be null.</param>
    public sealed record GetBookFormatsQuery(SearchParamsDTO SearchParams) : IQuery<List<BookFormatDTO>>;
}
