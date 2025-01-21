using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class AuthorBook : Entity
    {
        private AuthorBook(Guid id, Guid authorId, Guid bookId) : base(id)
        {
            AuthorID = authorId;
            BookID = bookId;
        }

        [Required]
        public Guid AuthorID { get; private set; }
        public Author Author { get; private set; }
        [Required]
        public Guid BookID { get; private set; }
        public Book Book { get; private set; }

        public static Result<AuthorBook> Create(Guid authorId, Guid bookId)
        {
            if(authorId == Guid.Empty)
            {
                return Result.Failure<AuthorBook>(new Error("AuthorID.Required", "AuthorID is required", ErrorType.Validation));
            }
            if (bookId == Guid.Empty)
            {
                return Result.Failure<AuthorBook>(new Error("BookID.Required", "BookID is required", ErrorType.Validation));
            }
            var authorBook = new AuthorBook(Guid.NewGuid(), authorId, bookId);
            authorBook.IsActive = true;
            return Result.Success(authorBook);
        }
    }
}
