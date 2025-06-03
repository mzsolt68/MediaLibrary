using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    public sealed class GetBooksOfFormatQueryHandler(IUnitOfWork context) : IQueryHandler<GetBooksOfFormatQuery, List<BookDTO>>
    {
        public async Task<Result<List<BookDTO>>> Handle(GetBooksOfFormatQuery request, CancellationToken cancellationToken)
        {
            if(!await context.BookFormatRepository.Exists(bf => bf.Id == request.BookFormatId))
            {
                return Result.Failure<List<BookDTO>>(new Error("Format.NotFound", $"BookFormat with Id {request.BookFormatId} not found.", ErrorType.NotFound));
            }
            var books = await context.BookFormatRepository.GetBooksOfFormat(request.BookFormatId, cancellationToken);
            if(books is null || books.Count() == 0)
            {
                return Result.Failure<List<BookDTO>>(new Error("Book.NotFound", "No books are found in the given format.", ErrorType.NotFound));
            }

            var bookDTOs = books.Select(b => b.AsBookDTO()).ToList();
            return Result.Success(bookDTOs);
        }
    }
}
