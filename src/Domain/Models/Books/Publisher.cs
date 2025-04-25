using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents a publisher entity in the domain.
    /// </summary>
    public class Publisher : Entity
    {
        private HashSet<Book> _publishedBooks;

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class with the specified ID and name.
        /// </summary>
        /// <param name="id">The unique identifier for the publisher.</param>
        /// <param name="publisherName">The name of the publisher.</param>
        private Publisher(Guid id, string publisherName) : base(id)
        {
            PublisherName = publisherName;
            _publishedBooks = new HashSet<Book>();
        }

        /// <summary>
        /// Gets or sets the name of the publisher.
        /// </summary>
        [Required]
        [Display(Name = "Kiadó")]
        public string PublisherName { get; set; }

        /// <summary>
        /// Gets the collection of books published by this publisher.
        /// </summary>
        public virtual ICollection<Book> PublishedBooks => _publishedBooks.ToList();

        /// <summary>
        /// Creates a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="publisherName">The name of the publisher to create.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="Publisher"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<Publisher> Create(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName))
            {
                return Result.Failure<Publisher>(new Error("PublisherName.Required", "Publisher name is required", ErrorType.Validation));
            }
            var publisher = new Publisher(Guid.NewGuid(), publisherName);
            publisher.IsActive = true;
            return Result.Success(publisher);
        }

        /// <summary>
        /// Updates the name of the publisher.
        /// </summary>
        /// <param name="publisherName">The new name of the publisher.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the update operation.
        /// </returns>
        public Result Update(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName))
            {
                return Result.Failure(new Error("PublisherName.Required", "Publisher name is required", ErrorType.Validation));
            }
            PublisherName = publisherName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
