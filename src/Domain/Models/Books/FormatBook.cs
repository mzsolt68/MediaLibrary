using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents the association between a Book and a Format.
    /// </summary>
    public class FormatBook : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatBook"/> class.
        /// It is used for EF Core only.
        /// </summary>
        private FormatBook() : base(Guid.Empty) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatBook"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the FormatBook entity.</param>
        /// <param name="formatId">The unique identifier of the associated Format.</param>
        /// <param name="bookId">The unique identifier of the associated Book.</param>
        private FormatBook(Guid id, Guid formatId, Guid bookId) : base(id)
        {
            FormatID = formatId;
            BookID = bookId;
        }

        /// <summary>
        /// Gets the unique identifier of the associated Format.
        /// </summary>
        [Required]
        public Guid FormatID { get; private set; }

        /// <summary>
        /// Gets the associated Format entity.
        /// </summary>
        public BookFormat Format { get; private set; }

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
        /// Creates a new instance of the <see cref="FormatBook"/> class.
        /// </summary>
        /// <param name="formatId">The unique identifier of the Format to associate.</param>
        /// <param name="bookId">The unique identifier of the Book to associate.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="FormatBook"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<FormatBook> Create(Guid formatId, Guid bookId)
        {
            if (formatId == Guid.Empty)
            {
                return Result.Failure<FormatBook>(new Error("FormatBook.FormatID.Empty", "Format ID is required", ErrorType.Validation));
            }
            if (bookId == Guid.Empty)
            {
                return Result.Failure<FormatBook>(new Error("FormatBook.BookID.Empty", "Book ID is required", ErrorType.Validation));
            }
            var formatBook = new FormatBook(Guid.NewGuid(), formatId, bookId);
            formatBook.IsActive = true;
            return Result.Success(formatBook);
        }
    }
}
