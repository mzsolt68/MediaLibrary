using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class FormatBook : Entity
    {
        private FormatBook(Guid id, Guid formatId, Guid bookId) : base(id)
        {
            FormatID = formatId;
            BookID = bookId;
        }

        [Required]
        public Guid FormatID { get; private set; }
        public BookFormat Format { get; private set; }
        [Required]
        public Guid BookID { get; private set; }
        public Book Book { get; private set; }

        public static Result<FormatBook> Create(Guid formatId, Guid bookId)
        {
            if(formatId == Guid.Empty)
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
