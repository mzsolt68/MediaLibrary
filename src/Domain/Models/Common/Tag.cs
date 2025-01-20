using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.Models.Common
{
    public class Tag : Entity
    {
        private HashSet<TagBook> _booksOfTag;

        private Tag(Guid id, string tagName) : base(id)
        {
            TagName = tagName;
            _booksOfTag = new HashSet<TagBook>();
        }
        [Required]
        [Display(Name = "Cimke")]
        public string TagName { get; private set; }

        public virtual ICollection<TagBook> BooksOfTag => _booksOfTag.ToList();

        public static Result<Tag> Create(string tagName)
        {
            if(string.IsNullOrWhiteSpace(tagName))
            {
                return Result.Failure<Tag>(new Error("TagName.Missing", "Tag name is missing", ErrorType.Validation));
            }
            var tag = new Tag(Guid.NewGuid(), tagName);
            tag.IsActive = true;
            return Result.Success(tag);
        }

        public void Update(string tagName)
        {
            TagName = tagName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
