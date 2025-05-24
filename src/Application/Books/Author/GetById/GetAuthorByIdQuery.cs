using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<BookAuthorDTO>;
}
