using Shouldly;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit tests for the Publisher entity
    /// </summary>
    public class PublisherTests
    {
        /// <summary>
        /// Proper parameters should create a new Publisher entity
        /// </summary>
        [Fact]
        public void ProperParametersShouldCreateNewPublisher()
        {
            // Arrange
            var publisherName = "Test Publisher";
            // Act
            var result = Publisher.Create(publisherName);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.PublisherName.ShouldBe(publisherName);
        }

        /// <summary>
        /// Empty publisher name should return a failure result
        /// <see cref="Publisher.Create(string)"/>
        /// </summary>
        [Fact]
        public void EmptyPublisherNameShouldReturnFailure()
        {
            // Arrange
            var publisherName = string.Empty;
            // Act
            var result = Publisher.Create(publisherName);
            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PublisherName.Required");
            result.Error.Message.ShouldBe("Publisher name is required");
        }

        /// <summary>
        /// Update should update the PublisherName property
        /// </summary>
        [Fact]
        public void UpdateShouldUpdatePublisherName()
        {
            // Arrange
            var publisherName = "Test Publisher";
            var publisher = Publisher.Create(publisherName).Value;
            var newPublisherName = "New Publisher Name";
            // Act
            var result = publisher.Update(newPublisherName);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            publisher.PublisherName.ShouldBe(newPublisherName);
        }

        /// <summary>
        /// Empty publisher name should return a failure result when updating
        /// </summary>
        [Fact]
        public void EmptyPublisherNameShouldReturnFailureWhenUpdating()
        {
            // Arrange
            var publisherName = "Test Publisher";
            var publisher = Publisher.Create(publisherName).Value;
            var newPublisherName = string.Empty;
            // Act
            var result = publisher.Update(newPublisherName);
            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PublisherName.Required");
            result.Error.Message.ShouldBe("Publisher name is required");
        }
    }
}
