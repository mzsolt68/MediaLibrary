using Domain.Models.Books;
using SharedKernel;
using Shouldly;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit tests for the BookFormat entity.
    /// </summary>
    public class BookFormatTests
    {
        /// <summary>
        /// Proper parameters should create a new BookFormat.
        /// </summary>
        [Fact]
        public void ProperParametersShouldCreateNewBookFormat()
        {
            // Arrange
            var bookFormatName = "Hardcover";
            var bookFormat = BookFormat.Create(bookFormatName);
            // Assert
            bookFormat.IsSuccess.ShouldBeTrue();
            bookFormat.IsFailure.ShouldBeFalse();
            bookFormat.Value.BookFormatName.ShouldBe(bookFormatName);
            bookFormat.Value.IsActive.ShouldBeTrue();
            bookFormat.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            bookFormat.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            bookFormat.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty name should fail to create a new BookFormat.
        /// </summary>
        [Fact]
        public void EmptyNameShouldFailToCreateNewBookFormat()
        {
            // Arrange
            var bookFormatName = string.Empty;
            var bookFormat = BookFormat.Create(bookFormatName);
            // Assert
            bookFormat.IsSuccess.ShouldBeFalse();
            bookFormat.IsFailure.ShouldBeTrue();
            bookFormat.Error.Code.ShouldBe("BookFormatName.Required");
            bookFormat.Error.Message.ShouldBe("Bookformat name is required.");
            bookFormat.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Proper parameters should update the BookFormat.
        /// </summary>
        [Fact]
        public void ProperParametersShouldUpdateBookFormat()
        {
            // Arrange
            var bookFormatName = "Hardcover";
            var bookFormat = BookFormat.Create(bookFormatName).Value;
            var newBookFormatName = "Paperback";
            // Act
            var result = bookFormat.Update(newBookFormatName);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            bookFormat.BookFormatName.ShouldBe(newBookFormatName);
            bookFormat.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            bookFormat.IsActive.ShouldBeTrue();
        }

        /// <summary>
        /// Empty name should fail to update the BookFormat.
        /// </summary>
        [Fact]
        public void EmptyNameShouldFailToUpdateBookFormat()
        {
            // Arrange
            var bookFormatName = "Hardcover";
            var bookFormat = BookFormat.Create(bookFormatName).Value;
            var newBookFormatName = string.Empty;
            // Act
            var result = bookFormat.Update(newBookFormatName);
            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            bookFormat.BookFormatName.ShouldBe(bookFormatName);
            bookFormat.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            bookFormat.IsActive.ShouldBeTrue();
            result.Error.Code.ShouldBe("BookFormatName.Required");
            result.Error.Message.ShouldBe("Bookformat name is required.");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }
    }
}
