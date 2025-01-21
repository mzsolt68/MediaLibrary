using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    public class BookFormat : Entity
    {
        private HashSet<FormatBook> bookFormats;

        private BookFormat(Guid guid, string bookFormatName) : base(guid)
        {
            BookFormatName = bookFormatName;
            bookFormats = new HashSet<FormatBook>();
        }
        [Required]
        [Display(Name = "Formátum")]
        public string BookFormatName { get; private set; }

        public virtual ICollection<FormatBook> BooksInFormat => bookFormats.ToList();

        public static Result<BookFormat> Create(string bookFormatName)
        {
            if(string.IsNullOrWhiteSpace(bookFormatName))
            {
                return Result.Failure<BookFormat>(new Error("BookFormatName.Required", "Bookformat name is required.", ErrorType.Validation));
            }
            var bookFormat = new BookFormat(Guid.NewGuid(), bookFormatName);
            bookFormat.IsActive = true;
            return Result.Success(bookFormat);
        }

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
