using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the query to retrieve detailed information about a specific book.
    /// </summary>
    /// <remarks>This query handler retrieves a book's details, including its associated data, based on the
    /// provided book ID. If the book is not found, an error result is returned indicating the absence of the
    /// book.</remarks>
    /// <param name="context">The unit of work used to access the book repository and perform the query.</param>
    public sealed class GetBookWithDetailsQueryHandler(IUnitOfWork context) : IQueryHandler<GetBookWithDetailsQuery, BookDetailsDTO>
    {
        /// <summary>
        /// Handles the query to retrieve detailed information about a specific book.
        /// </summary>
        /// <remarks>This method attempts to retrieve a book with its full details from the repository. If
        /// the book is not found, the result will indicate failure with an error of type <c>NotFound</c>.</remarks>
        /// <param name="request">The query containing the ID of the book to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a <see cref="BookDetailsDTO"/> with the book's details if found;
        /// otherwise, a failure result with an appropriate error message.</returns>
        public async Task<Result<BookDetailsDTO>> Handle(GetBookWithDetailsQuery request, CancellationToken cancellationToken)
        {
            Book? book = await context.BookRepository.GetBookWithFullDataAsync(request.BookId, cancellationToken);
            if (book is null)
            {
                return Result.Failure<BookDetailsDTO>(new Error(
                    "Book.NotFound", $"The book with ID {request.BookId} was not found.", ErrorType.NotFound));
            }
            return Result.Success(book.AsBookDetailsDTO());
        }
    }
}
