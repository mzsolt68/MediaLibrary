using Application.Abstractions.Data;
using Application.Common;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;

namespace Application.UnitTests.Common
{
    public class GetLanguageByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILanguageRepository> _languageRepository;

        public GetLanguageByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _languageRepository = new Mock<ILanguageRepository>();
            _unitOfWork.Setup(x => x.LanguageRepository).Returns(_languageRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnLanguageDTO_WhenLanguageExists()
        {
            // Arrange
            var language = Language.Create("English").Value;
            _languageRepository.Setup(x => x.GetByIdAsync(language.Id)).ReturnsAsync(language);

            var query = new GetLanguageByIdQuery(language.Id);
            var handler = new GetLanguageByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.GetType().ShouldBe(typeof(LanguageDTO));
            result.Value.LanguageName.ShouldBe("English");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenLanguageDoesNotExist()
        {
            // Arrange
            var languageId = Guid.NewGuid();
            _languageRepository.Setup(x => x.GetByIdAsync(languageId)).ReturnsAsync((Language?)null);

            var query = new GetLanguageByIdQuery(languageId);
            var handler = new GetLanguageByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Language.NotFound");
        }
    }
}
