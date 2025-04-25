using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.Models.Common
{
    /// <summary>
    /// Represents a tag entity in the domain.
    /// </summary>
    public class Tag : Entity
    {
        private HashSet<TagBook> _booksOfTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class with the specified identifier and tag name.
        /// </summary>
        /// <param name="id">The unique identifier for the tag.</param>
        /// <param name="tagName">The name of the tag.</param>
        private Tag(Guid id, string tagName) : base(id)
        {
            TagName = tagName;
            _booksOfTag = new HashSet<TagBook>();
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        [Required]
        [Display(Name = "Cimke")]
        public string TagName { get; private set; }

        /// <summary>
        /// Gets the collection of books associated with the tag.
        /// </summary>
        public virtual ICollection<TagBook> BooksOfTag => _booksOfTag.ToList();

        /// <summary>
        /// Creates a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="tagName">The name of the tag to create.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="Tag"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<Tag> Create(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                return Result.Failure<Tag>(new Error("TagName.Missing", "Tag name is missing", ErrorType.Validation));
            }
            var tag = new Tag(Guid.NewGuid(), tagName);
            tag.IsActive = true;
            return Result.Success(tag);
        }

        /// <summary>
        /// Updates the name of the tag.
        /// </summary>
        /// <param name="tagName">The new name of the tag.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the update operation.
        /// </returns>
        public Result Update(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                return Result.Failure(new Error("TagName.Missing", "Tag name is missing", ErrorType.Validation));
            }
            TagName = tagName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
