using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Common;
using SharedKernel;

namespace Domain.Models.Books
{
    public class TagBook : Entity
    {
        private TagBook(Guid id, Guid bookId, Guid tagId) : base(id)
        {
            BookID = bookId;
            TagID = tagId;
        }

        [Required]
        public Guid BookID { get; private set; }
        public Book Book { get; private set; }
        [Required]
        public Guid TagID { get; private set; }
        public Tag Tag { get; private set; }

        public static TagBook Create(Guid bookId, Guid tagId)
        {
            var tagBook = new TagBook(Guid.NewGuid(), bookId, tagId);
            tagBook.IsActive = true;
            return tagBook;
        }
    }
}
