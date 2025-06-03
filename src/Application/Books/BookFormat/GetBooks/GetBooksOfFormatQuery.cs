using Application.Abstractions.Messaging;
using Application.Dto.Books;

namespace Application.Books
{
    public sealed record GetBooksOfFormatQuery(Guid BookFormatId) : IQuery<List<BookDTO>>;
}
