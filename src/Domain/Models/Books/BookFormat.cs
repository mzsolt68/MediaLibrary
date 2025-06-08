using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents the format of a book in the domain.
    /// </summary>
    public class BookFormat : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookFormat"/> class.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private BookFormat(Guid id) : base(id)
        {
            _bookFormats = []; 
        }

        private HashSet<Book> _bookFormats;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookFormat"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier for the book format.</param>
        /// <param name="bookFormatName">The name of the book format.</param>
        private BookFormat(Guid guid, string bookFormatName) : base(guid)
        {
            BookFormatName = bookFormatName;
            _bookFormats = [];
        }

        /// <summary>
        /// Gets the name of the book format.
        /// </summary>
        [Required]
        [Display(Name = "Formátum")]
        public string BookFormatName { get; private set; }

        /// <summary>
        /// Gets the collection of books associated with this format.
        /// </summary>
        public virtual ICollection<Book> BooksInFormat => _bookFormats.ToList();

        /// <summary>
        /// Creates a new instance of the <see cref="BookFormat"/> class.
        /// </summary>
        /// <param name="bookFormatName">The name of the book format.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="BookFormat"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<BookFormat> Create(string bookFormatName)
        {
            if (string.IsNullOrWhiteSpace(bookFormatName))
            {
                return Result.Failure<BookFormat>(new Error("BookFormatName.Required", "Bookformat name is required.", ErrorType.Validation));
            }
            var bookFormat = new BookFormat(Guid.NewGuid(), bookFormatName);
            bookFormat.IsActive = true;
            return Result.Success(bookFormat);
        }

        /// <summary>
        /// Updates the name of the book format.
        /// </summary>
        /// <param name="bookFormatName">The new name of the book format.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the update operation.
        /// </returns>
        public Result Update(string bookFormatName)
        {
            if (string.IsNullOrWhiteSpace(bookFormatName))
            {
                return Result.Failure(new Error("BookFormatName.Required", "Bookformat name is required.", ErrorType.Validation));
            }
            BookFormatName = bookFormatName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
