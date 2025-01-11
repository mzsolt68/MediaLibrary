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

        public static BookFormat Create(string bookFormatName)
        {
            var bookFormat = new BookFormat(Guid.NewGuid(), bookFormatName);
            bookFormat.IsActive = true;
            return bookFormat;
        }

        public void Update(string bookFormatName)
        {
            BookFormatName = bookFormatName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
