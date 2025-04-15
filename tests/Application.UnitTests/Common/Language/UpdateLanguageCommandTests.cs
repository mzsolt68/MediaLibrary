using Shouldly;
using Moq;
using Application.Common;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Common;
using SharedKernel;

namespace Application.UnitTests.Common
{
    public class UpdateLanguageCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<DbSet<Language>> _languageDbSet;

        public UpdateLanguageCommandHandlerTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _languageDbSet = new Mock<DbSet<Language>>();
            _context.Setup(x => x.Languages).Returns(_languageDbSet.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateLanguage_WhenLanguageExists()
        {
            // Arrange
            var language = Language.Create("Old Name").Value;
            _languageDbSet.Setup(x => x.FindAsync(language.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(language);

            var command = new UpdateLanguageCommand(language.Id, "New Name");
            var handler = new UpdateLanguageCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            language.LanguageName.ShouldBe("New Name");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenLanguageDoesNotExist()
        {
            // Arrange
            var languageId = Guid.NewGuid();
            _languageDbSet.Setup(x => x.FindAsync(languageId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Language)null);

            var command = new UpdateLanguageCommand(languageId, "New Name");
            var handler = new UpdateLanguageCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Language.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenLanguageNameIsInvalid()
        {
            // Arrange
            var language = Language.Create("Old Name").Value;
            _languageDbSet.Setup(x => x.FindAsync(language.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(language);

            var command = new UpdateLanguageCommand(language.Id, string.Empty);
            var handler = new UpdateLanguageCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("LanguageName.Missing");
        }
    }
}
