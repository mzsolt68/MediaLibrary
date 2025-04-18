using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    public class Author : Entity
    {
        private HashSet<AuthorBook> _books;

        private Author(Guid id, string lastName, string firstName, string middleName) : base(id)
        {
            AuthorLastName = lastName;
            AuthorFirstName = firstName;
            AuthorMiddleName = middleName;
            _books = new HashSet<AuthorBook>();
        }
        [Required]
        [Display(Name = "Vezetéknév")]
        public string AuthorLastName { get; private set; }
        [Display(Name = "Keresztnév")]
        public string AuthorFirstName { get; private set; }
        [Display(Name = "Középső név")]
        public string AuthorMiddleName { get; private set; }

        public virtual ICollection<AuthorBook> Books => _books.ToList();

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
