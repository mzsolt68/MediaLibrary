using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles queries to retrieve a list of books in a specific format.
    /// </summary>
    /// <remarks>This query handler checks if the specified book format exists before retrieving the books. If
    /// the format does not exist or no books are found in the given format, an appropriate failure result is
    /// returned.</remarks>
    /// <param name="context">The unit of work providing access to the repositories required for the query.</param>
    public sealed class GetBooksOfFormatQueryHandler(IUnitOfWork context) : IQueryHandler<GetBooksOfFormatQuery, List<BookDTO>>
    {
        /// <summary>
        /// Handles the query to retrieve a list of books in a specified format.
        /// </summary>
        /// <remarks>This method checks whether the specified book format exists before attempting to
        /// retrieve the books.  If the format does not exist, a failure result is returned with an appropriate error
        /// message.  If no books are found for the given format, a failure result is also returned.</remarks>
        /// <param name="request">The query containing the identifier of the book format to retrieve books for.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="BookDTO"/> objects representing the books in the
        /// specified format,  or a failure result if the format does not exist or no books are found.</returns>
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
