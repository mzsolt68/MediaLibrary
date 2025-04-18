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

        /// <summary>
        /// Proper parameters should update author.
        /// <see cref="Author.Update(string, string, string)"/>
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateAuthor()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var middleName = "Michael";
            var author = Author.Create(lastName, firstName, middleName).Value;
            // Act
            var result = author.Update("Smith", "Jane", "Marie");
            // Assert
            result.IsSuccess.ShouldBeTrue();
            author.AuthorLastName.ShouldBe("Smith");
            author.AuthorFirstName.ShouldBe("Jane");
            author.AuthorMiddleName.ShouldBe("Marie");
        }

        /// <summary>
        /// Missing last name should return error.
        /// <see cref="Author.Update(string, string, string)"/>
        /// </summary>
        [Fact]
        public void Update_EmptyLastName_ShouldReturnError()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var middleName = "Michael";
            var author = Author.Create(lastName, firstName, middleName).Value;
            // Act
            var result = author.Update(string.Empty, "Jane", "Marie");
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Author.Lastname.Required");
            result.Error.Message.ShouldBe("Author last name is required.");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }
    }
}
