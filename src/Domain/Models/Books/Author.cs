using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents an author in the domain.
    /// </summary>
    public class Author : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class with the specified identifier.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private Author(Guid id) : base(id) {}

        private HashSet<AuthorBook> _books;

        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="middleName">The middle name of the author.</param>
        private Author(Guid id, string lastName, string firstName, string middleName) : base(id)
        {
            AuthorLastName = lastName;
            AuthorFirstName = firstName;
            AuthorMiddleName = middleName;
            _books = new HashSet<AuthorBook>();
        }

        /// <summary>
        /// Gets the last name of the author.
        /// </summary>
        [Required]
        [Display(Name = "Vezetéknév")]
        public string AuthorLastName { get; private set; }

        /// <summary>
        /// Gets the first name of the author.
        /// </summary>
        [Display(Name = "Keresztnév")]
        public string AuthorFirstName { get; private set; }

        /// <summary>
        /// Gets the middle name of the author.
        /// </summary>
        [Display(Name = "Középső név")]
        public string AuthorMiddleName { get; private set; }

        /// <summary>
        /// Gets the collection of books associated with the author.
        /// </summary>
        public virtual ICollection<AuthorBook> Books => _books.ToList();

        /// <summary>
        /// Creates a new instance of the <see cref="Author"/> class.
        /// </summary>
        /// <param name="lastName">The last name of the author.</param>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="middleName">The middle name of the author.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="Author"/> instance if successful, or an error if validation fails.</returns>
        public static Result<Author> Create(string lastName, string firstName, string middleName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<Author>(new Error("Author.Lastname.Required", "Author last name is required.", ErrorType.Validation));
            }
            var author = new Author(Guid.NewGuid(), lastName, firstName, middleName);
            author.IsActive = true;
            return Result.Success(author);
        }

        /// <summary>
        /// Updates the properties of the author.
        /// </summary>
        /// <param name="lastName">The new last name of the author.</param>
        /// <param name="firstName">The new first name of the author.</param>
        /// <param name="middleName">The new middle name of the author.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure of the update operation.</returns>
        public Result Update(string lastName, string firstName, string middleName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure(new Error("Author.Lastname.Required", "Author last name is required.", ErrorType.Validation));
            }
            AuthorLastName = lastName;
            AuthorFirstName = firstName;
            AuthorMiddleName = middleName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
