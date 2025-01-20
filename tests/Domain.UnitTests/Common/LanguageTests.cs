using Domain.Models.Common;
using SharedKernel;
using Shouldly;

namespace Domain.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the <see cref="Language"/> entity.
    /// </summary>
    public class LanguageTests
    {
        /// <summary>
        /// Tests the <see cref="Language.Create(string)"/> method.
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnLanguage()
        {
            // Arrange
            var languageName = "English";
            // Act
            var result = Language.Create(languageName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.LanguageName.ShouldBe(languageName);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }
        /// <summary>
        /// Tests the <see cref="Language.Create(string)"/> method with an empty language name.
        /// </summary>
        [Fact]
        public void EmptyLanguageNameShouldReturnError()
        {
            // Arrange
            var languageName = "";
            // Act
            var result = Language.Create(languageName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("LanguageName.Missing");
            result.Error.Message.ShouldBe("Language name is missing");
        }

        /// <summary>
        /// Tests the <see cref="Language.Update(string)"/> method.
        /// </summary>
        [Fact]
        public void UpdateShouldChangeLanguageName()
        {
            // Arrange
            var languageName = "English";
            var language = Language.Create(languageName).Value;
            var newLanguageName = "Hungarian";
            // Act
            language.Update(newLanguageName);
            // Assert
            language.LanguageName.ShouldBe(newLanguageName);
            language.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            language.IsActive.ShouldBeTrue();
            language.ShouldNotBeNull();
        }
    }
}
