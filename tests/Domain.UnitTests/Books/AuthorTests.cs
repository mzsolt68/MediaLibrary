using Shouldly;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit tests for the <see cref="Author"/> entity.
    /// </summary>
    public class AuthorTests
    {
        /// <summary>
        /// Tests the <see cref="Author.Create(string, string, string)"/> method.
        /// </summary>
        [Fact]
        public void Create_ShouldReturnAuthor()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var middleName = "Michael";
            // Act
            var result = Author.Create(lastName, firstName, middleName);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.AuthorLastName.ShouldBe(lastName);
            result.Value.AuthorFirstName.ShouldBe(firstName);
            result.Value.AuthorMiddleName.ShouldBe(middleName);
        }

        /// <summary>
        /// Empty last name should return error.
        /// <see cref="Author.Create(string, string, string)"/>
        /// </summary>
        [Fact]
        public void Create_EmptyLastName_ShouldReturnError()
        {
            // Arrange
            var lastName = string.Empty;
            var firstName = "John";
            var middleName = "Michael";
            // Act
            var result = Author.Create(lastName, firstName, middleName);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Author.Lastname.Required");
            result.Error.Message.ShouldBe("Author last name is required.");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }
    }
}
