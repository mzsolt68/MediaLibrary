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

        public static FormatBook Create(Guid formatId, Guid bookId)
        {
            var formatBook = new FormatBook(Guid.NewGuid(), formatId, bookId);
            formatBook.IsActive = true;
            return formatBook;
        }
    }
}
