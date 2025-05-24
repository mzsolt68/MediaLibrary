using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;

namespace Application.Books
{
    /// <summary>
    /// Represents a query to retrieve a list of books based on the specified search parameters.
    /// </summary>
    /// <param name="SearchParams">The search parameters used to filter the books. This parameter must not be <see langword="null"/>.</param>
    public sealed record GetBooksQuery(SearchParamsDTO SearchParams) : IQuery<List<BookDTO>>;
}
