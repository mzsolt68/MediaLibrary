using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the query to retrieve a book format by its unique identifier.
    /// </summary>
    /// <remarks>This query handler retrieves a book format from the repository using the provided identifier.
    /// If the book format is not found, a failure result is returned with an appropriate error message.</remarks>
    /// <param name="context"></param>
    public sealed class GetBookFormatByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetBookFormatByIdQuery, BookFormatDTO>
    {
        /// <summary>
        /// Handles the query to retrieve a book format by its unique identifier.
        /// </summary>
        /// <param name="request">The query containing the unique identifier of the book format to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing the <see cref="BookFormatDTO"/> if the book format is found; otherwise,
        /// a failure result with an appropriate error message.</returns>
        public async Task<Result<BookFormatDTO>> Handle(GetBookFormatByIdQuery request, CancellationToken cancellationToken)
        {
            var bookFormat = await context.BookFormatRepository.GetByIdAsync(request.BookFormatId, cancellationToken);
            if (bookFormat == null)
            {
                return Result.Failure<BookFormatDTO>(new Error("BookFormat.NotFound", $"BookFormat with ID {request.BookFormatId} was not found.", ErrorType.NotFound));
            }
            return Result.Success(bookFormat.AsBookFormatDTO());
        }
    }
}
