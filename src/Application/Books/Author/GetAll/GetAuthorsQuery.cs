using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a list of authors based on the specified search parameters.
    /// </summary>
    /// <param name="SearchParams">The search parameters used to filter the authors. This includes criteria such as name, genre, or other relevant
    /// filters.</param>
    public sealed record GetAuthorsQuery(SearchParamsDTO SearchParams) : IQuery<List<BookAuthorDTO>>;
}
