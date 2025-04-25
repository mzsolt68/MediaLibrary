using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents the association between an Author and a Book.
    /// </summary>
    public class AuthorBook : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorBook"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the AuthorBook entity.</param>
        /// <param name="authorId">The unique identifier of the associated Author.</param>
        /// <param name="bookId">The unique identifier of the associated Book.</param>
        private AuthorBook(Guid id, Guid authorId, Guid bookId) : base(id)
        {
            AuthorID = authorId;
            BookID = bookId;
        }

        /// <summary>
        /// Gets the unique identifier of the associated Author.
        /// </summary>
        [Required]
        public Guid AuthorID { get; private set; }

        /// <summary>
        /// Gets the associated Author entity.
        /// </summary>
        public Author Author { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the associated Book.
        /// </summary>
        [Required]
        public Guid BookID { get; private set; }

        /// <summary>
        /// Gets the associated Book entity.
        /// </summary>
        public Book Book { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AuthorBook"/> class.
        /// </summary>
        /// <param name="authorId">The unique identifier of the Author to associate.</param>
        /// <param name="bookId">The unique identifier of the Book to associate.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="AuthorBook"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<AuthorBook> Create(Guid authorId, Guid bookId)
        {
            if (authorId == Guid.Empty)
            {
                return Result.Failure<AuthorBook>(new Error("AuthorID.Required", "AuthorID is required", ErrorType.Validation));
            }
            if (bookId == Guid.Empty)
            {
                return Result.Failure<AuthorBook>(new Error("BookID.Required", "BookID is required", ErrorType.Validation));
            }
            var authorBook = new AuthorBook(Guid.NewGuid(), authorId, bookId);
            authorBook.IsActive = true;
            return Result.Success(authorBook);
        }
    }
}
