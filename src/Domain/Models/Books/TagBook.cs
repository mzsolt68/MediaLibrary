using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Common;
using SharedKernel;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents the association between a Book and a Tag.
    /// </summary>
    public class TagBook : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagBook"/> class with an empty identifier.
        /// It is used for EF Core only.
        /// </summary>
        private TagBook() : base(Guid.Empty) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBook"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the TagBook entity.</param>
        /// <param name="bookId">The unique identifier of the associated Book.</param>
        /// <param name="tagId">The unique identifier of the associated Tag.</param>
        private TagBook(Guid id, Guid bookId, Guid tagId) : base(id)
        {
            BookID = bookId;
            TagID = tagId;
        }

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
        /// Gets the unique identifier of the associated Tag.
        /// </summary>
        [Required]
        public Guid TagID { get; private set; }

        /// <summary>
        /// Gets the associated Tag entity.
        /// </summary>
        public Tag Tag { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="TagBook"/> class.
        /// </summary>
        /// <param name="bookId">The unique identifier of the Book to associate.</param>
        /// <param name="tagId">The unique identifier of the Tag to associate.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="TagBook"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<TagBook> Create(Guid bookId, Guid tagId)
        {
            if (bookId == Guid.Empty)
            {
                return Result.Failure<TagBook>(new Error("BookID.Missing", "BookID is required", ErrorType.Validation));
            }
            if (tagId == Guid.Empty)
            {
                return Result.Failure<TagBook>(new Error("TagID.Missing", "TagID is required", ErrorType.Validation));
            }
            var tagBook = new TagBook(Guid.NewGuid(), bookId, tagId);
            tagBook.IsActive = true;
            return Result.Success(tagBook);
        }
    }
}
