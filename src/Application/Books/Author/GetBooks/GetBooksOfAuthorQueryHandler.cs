using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the query to retrieve details of an author and their associated books.
    /// </summary>
    /// <remarks>This query handler processes a <see cref="GetBooksOfAuthorQuery"/> to fetch the author's
    /// details  and a list of their books. If the specified author is not found, the operation will return a failure
    /// result.</remarks>
    /// <param name="context">The unit of work providing access to the repositories required for the query.</param>
    public sealed class GetBooksOfAuthorQueryHandler(IUnitOfWork context) : IQueryHandler<GetBooksOfAuthorQuery, BookAuthorDetailsDTO>
    {
        /// <summary>
        /// Handles the query to retrieve details of an author and their associated books.
        /// </summary>
        /// <remarks>If the specified author does not exist, the method returns a failure result with an
        /// error of type <c>ErrorType.NotFound</c>.</remarks>
        /// <param name="request">The query containing the ID of the author whose details and books are to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a <see cref="BookAuthorDetailsDTO"/> object with the author's details
        /// and their books if the author is found; otherwise, a failure result with an appropriate error message.</returns>
        public async Task<Result<BookAuthorDetailsDTO>> Handle(GetBooksOfAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await context.AuthorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
            if (author is null)
            {
                return Result.Failure<BookAuthorDetailsDTO>(new Error(
                    "Author.NotFound", $"Author with ID {request.AuthorId} was not found.", ErrorType.NotFound));
            }
            var books = await context.AuthorRepository.GetBooksAsync(request.AuthorId, cancellationToken);
            BookAuthorDetailsDTO result = new BookAuthorDetailsDTO
            {
                Author = author.AsAuthorDTO(),
                Books = books.Select(x => x.AsBookDTO()).ToList()
            };
            return Result.Success(result);
        }
    }
}
