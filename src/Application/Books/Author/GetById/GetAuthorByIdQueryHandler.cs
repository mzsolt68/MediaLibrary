using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    public sealed class GetAuthorByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetAuthorByIdQuery, BookAuthorDTO>
    {
        public async Task<Result<BookAuthorDTO>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await context.AuthorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
            if (author == null)
            {
                return Result.Failure<BookAuthorDTO>(new Error("Author.NotFound", "The author was not found in the database.", ErrorType.NotFound));
            }
            return Result.Success(author.AsAuthorDTO());
        }
    }
}
