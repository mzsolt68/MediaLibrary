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
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<DbSet<Language>> _languages;

        public DeleteLanguageCommandTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _languages = new Mock<DbSet<Language>>();
            _context.Setup(x => x.Languages).Returns(_languages.Object);
        }

        /// <summary>
        /// Proper parameters should set language to inactive.
        /// </summary>
        [Fact]
        public async Task ProperParametersShouldSetLanguageToInactive()
        {
            // Arrange
            var language = Language.Create("English").Value;
            _languages.Setup(x => x.FindAsync(language.Id, CancellationToken.None)).ReturnsAsync(language);
            var command = new DeleteLanguageCommand(language.Id);

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
            _languages.Setup(x => x.FindAsync(languageId, CancellationToken.None)).ReturnsAsync((Language)null);
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
