using Domain.Models.Books;
using SharedKernel;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit tests for the FormatBook entity.
    /// </summary>
    public class FormatBookTests
    {
        /// <summary>
        /// Proper parameters should create a new FormatBook.
        /// </summary>
        [Fact]
        public void ProperParametersShouldCreateNewFormatBook()
        {
            // Arrange
            var formatId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            // Act
            var formatBook = FormatBook.Create(formatId, bookId);
            // Assert
            formatBook.IsSuccess.ShouldBeTrue();
            formatBook.IsFailure.ShouldBeFalse();
            formatBook.Value.FormatID.ShouldBe(formatId);
            formatBook.Value.BookID.ShouldBe(bookId);
            formatBook.Value.IsActive.ShouldBeTrue();
            formatBook.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            formatBook.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            formatBook.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty format id should return failure.
        /// </summary>
        [Fact]
        public void EmptyFormatIdShouldReturnFailure()
        {
            // Arrange
            var formatId = Guid.Empty;
            var bookId = Guid.NewGuid();
            // Act
            var formatBook = FormatBook.Create(formatId, bookId);
            // Assert
            formatBook.IsSuccess.ShouldBeFalse();
            formatBook.IsFailure.ShouldBeTrue();
            formatBook.Error.Code.ShouldBe("FormatBook.FormatID.Empty");
            formatBook.Error.Message.ShouldBe("Format ID is required");
            formatBook.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Empty book id should return failure.
        /// </summary>
        [Fact]
        public void EmptyBookIdShouldReturnFailure()
        {
            // Arrange
            var formatId = Guid.NewGuid();
            var bookId = Guid.Empty;
            // Act
            var formatBook = FormatBook.Create(formatId, bookId);
            // Assert
            formatBook.IsSuccess.ShouldBeFalse();
            formatBook.IsFailure.ShouldBeTrue();
            formatBook.Error.Code.ShouldBe("FormatBook.BookID.Empty");
            formatBook.Error.Message.ShouldBe("Book ID is required");
            formatBook.Error.Type.ShouldBe(ErrorType.Validation);
        }
    }
}
