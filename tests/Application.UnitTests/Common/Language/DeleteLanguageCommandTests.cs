using Application.Common;
using Application.Abstractions.Data;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the DeleteLanguageCommandHandler class.
    /// </summary>
    public class DeleteLanguageCommandTests
    {
        private readonly Mock<IUnitOfWork> _context;
        private readonly Mock<ILanguageRepository> _languages;

        public DeleteLanguageCommandTests()
        {
            _context = new Mock<IUnitOfWork>();
            _languages = new Mock<ILanguageRepository>();
            _context.Setup(x => x.LanguageRepository).Returns(_languages.Object);
        }

        /// <summary>
        /// Proper parameters should set language to inactive.
        /// </summary>
        [Fact]
        public async Task ProperParametersShouldSetLanguageToInactive()
        {
            // Arrange
            var language = Language.Create("English").Value;
            var command = new DeleteLanguageCommand(language.Id);
            _languages.Setup(x => x.GetByIdAsync(language.Id)).ReturnsAsync(language);
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new DeleteLanguageCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            language.IsActive.ShouldBeFalse();
        }

        /// <summary>
        /// Missing language ID should return failure result.
        /// </summary>
        [Fact]
        public async Task MissingLanguageIdShouldReturnFailureResult()
        {
            // Arrange
            var languageId = Guid.Empty;
            _languages.Setup(x => x.GetByIdAsync(languageId)).ReturnsAsync((Language)null);
            var command = new DeleteLanguageCommand(languageId);

            // Act
            var handler = new DeleteLanguageCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Language.NotFound");
        }
    }
}
