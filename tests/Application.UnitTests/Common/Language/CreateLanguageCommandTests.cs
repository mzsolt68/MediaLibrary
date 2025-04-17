using Shouldly;
using Moq;
using Application.Common;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Common;
using SharedKernel;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the CreateLanguageCommandHandler class.
    /// </summary>
    public class CreateLanguageCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _context;

        public CreateLanguageCommandHandlerTests()
        {
            _context = new Mock<IUnitOfWork>();
            _context.Setup(x => x.LanguageRepository).Returns(new Mock<ILanguageRepository>().Object);
        }

        /// <summary>
        /// Proper parameters should create a new language
        /// <see cref="CreateLanguageCommand"/>
        /// </summary>
        [Fact]
        public async Task ProperParametersShouldCreateNewLanguage()
        {
            // Arrange
            var languageName = "English";
            var command = new CreateLanguageCommand
            {
                LanguageName = languageName
            };
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new CreateLanguageCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<Guid>();
        }

        /// <summary>
        /// Missing language name should return a failure result
        /// <see cref="CreateLanguageCommand"/>
        /// </summary>
        [Fact]
        public async Task MissingLanguageNameShouldReturnFailureResult()
        {
            // Arrange
            var languageName = string.Empty; // Invalid name
            var command = new CreateLanguageCommand
            {
                LanguageName = languageName
            };

            // Act
            var handler = new CreateLanguageCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("LanguageName.Missing");
        }
    }
}
