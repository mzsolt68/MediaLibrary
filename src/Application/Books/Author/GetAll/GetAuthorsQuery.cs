using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;

namespace Application.Books
{
    public sealed record GetAuthorsQuery(SearchParamsDTO SearchParams) : IQuery<List<BookAuthorDTO>>;
}
