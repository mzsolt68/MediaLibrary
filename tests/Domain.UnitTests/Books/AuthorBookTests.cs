using Shouldly;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit test for AuthorBook entity
    /// </summary>
    public class AuthorBookTests
    {
        /// <summary>
        /// Proper parameters should create AuthorBook
        /// </summary>
        [Fact]
        public void ProperParametersShouldCreateAuthorBook()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            // Act
            var authorBook = AuthorBook.Create(authorId, bookId);
            // Assert
            authorBook.IsSuccess.ShouldBeTrue();
            authorBook.IsFailure.ShouldBeFalse();
            authorBook.Value.AuthorID.ShouldBe(authorId);
            authorBook.Value.BookID.ShouldBe(bookId);
            authorBook.Value.IsActive.ShouldBeTrue();
            authorBook.Value.Id.ShouldNotBe(Guid.Empty);
            authorBook.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.Now);
            authorBook.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.Now);
        }

        /// <summary>
        /// Empty authorId should return failure
        /// </summary>
        [Fact]
        public void EmptyAuthorIdShouldReturnFailure()
        {
            // Arrange
            var authorId = Guid.Empty;
            var bookId = Guid.NewGuid();
            // Act
            var authorBook = AuthorBook.Create(authorId, bookId);
            // Assert
            authorBook.IsSuccess.ShouldBeFalse();
            authorBook.IsFailure.ShouldBeTrue();
            authorBook.Error.Type.ShouldBe(ErrorType.Validation);
            authorBook.Error.Code.ShouldBe("AuthorID.Required");
            authorBook.Error.Message.ShouldBe("AuthorID is required");
        }

        /// <summary>
        /// Empty bookId should return failure
        /// </summary>
        [Fact]
        public void EmptyBookIdShouldReturnFailure()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var bookId = Guid.Empty;
            // Act
            var authorBook = AuthorBook.Create(authorId, bookId);
            // Assert
            authorBook.IsSuccess.ShouldBeFalse();
            authorBook.IsFailure.ShouldBeTrue();
            authorBook.Error.Type.ShouldBe(ErrorType.Validation);
            authorBook.Error.Code.ShouldBe("BookID.Required");
            authorBook.Error.Message.ShouldBe("BookID is required");
        }
    }
}
