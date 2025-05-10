using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the creation of a new book.
    /// </summary>
    internal sealed class CreateBookCommandHandler(IUnitOfWork context) : ICommandHandler<CreateBookCommand, Guid>
    {
        /// <summary>
        /// Handles the command to create a new book.
        /// </summary>
        /// <param name="request">The command containing the details of the book to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the result of the operation,
        /// including the unique identifier of the created book if successful.
        /// </returns>
        public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            // Create a new book instance.
            var book = Book.Create(
                request.BookTitle,
                request.Edition,
                request.PublisherID,
                request.PublishYear,
                request.ISBN,
                request.LanguageID
            );

            // Return failure if book creation fails.
            if (book.IsFailure)
            {
                return Result.Failure<Guid>(book.Error);
            }

            // Add authors to the book.
            foreach (var authorID in request.AuthorIDs)
            {
                var author = await context.AuthorRepository.GetByIdAsync(authorID);
                var authorBook = book.Value.AddAuthor(author);
                if (authorBook.IsFailure)
                {
                    return Result.Failure<Guid>(authorBook.Error);
                }
            }

            // Add formats to the book.
            foreach (var formatID in request.FormatIDs)
            {
                var format = await context.BookFormatRepository.GetByIdAsync(formatID);
                var formatBook = book.Value.AddFormat(format);
                if (formatBook.IsFailure)
                {
                    return Result.Failure<Guid>(formatBook.Error);
                }
            }

            // Add tags to the book.
            foreach (var tagID in request.TagIDs)
            {
                var tag = await context.TagRepository.GetByIdAsync(tagID);
                var tagBook = book.Value.AddTag(tag);
                if (tagBook.IsFailure)
                {
                    return Result.Failure<Guid>(tagBook.Error);
                }
            }

            // Add the book to the repository.
            context.BookRepository.Add(book.Value);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);

            // Return failure if saving changes fails.
            if (result == 0)
            {
                return Result.Failure<Guid>(new Error("Book.CreateFailed", "Failed to create the book.", ErrorType.Failure));
            }

            // Return success with the book's unique identifier.
            return Result.Success(book.Value.Id);
        }
    }
}