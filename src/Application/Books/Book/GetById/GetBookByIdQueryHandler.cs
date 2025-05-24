using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the query to retrieve a book by its unique identifier.
    /// </summary>
    /// <remarks>This query handler retrieves a book from the data source using the provided book ID.  If the
    /// book is not found, a failure result is returned with an appropriate error message.</remarks>
    /// <param name="context">The unit of work used to access the book repository.</param>
    public sealed class GetBookByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetBookByIdQuery, BookDTO>
    {
        /// <summary>
        /// Handles the retrieval of a book by its unique identifier.
        /// </summary>
        /// <param name="request">The query containing the unique identifier of the book to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a <see cref="BookDTO"/> if the book is found; otherwise, a failure
        /// result with an error indicating that the book was not found.</returns>
        public async Task<Result<BookDTO>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Book? book = await context.BookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if(book is null)
            {
                return Result.Failure<BookDTO>(new Error(
                    "Book.NotFound", $"The book with Id {request.BookId} was not found.", ErrorType.NotFound));
            }
            return Result.Success(book.AsBookDTO(true));
        }
    }
}
