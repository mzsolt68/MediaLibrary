using Application.Abstractions.Data;
using Application.Common;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;

namespace Application.UnitTests.Common
{
    public class GetLanguagesQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILanguageRepository> _languageRepository;

        public GetLanguagesQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _languageRepository = new Mock<ILanguageRepository>();
            _unitOfWork.Setup(x => x.LanguageRepository).Returns(_languageRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfLanguageDTOs_WhenLanguagesExist()
        {
            // Arrange
            var languages = new List<Language>
            {
                Language.Create("English").Value,
                Language.Create("Spanish").Value
            };
            _languageRepository.Setup(x => x.GetAllAsync(false)).ReturnsAsync(languages);

            var query = new GetLanguagesQuery();
            var handler = new GetLanguagesQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.GetType().ShouldBe(typeof(List<LanguageDTO>));
            result.Value[0].LanguageName.ShouldBe("English");
            result.Value[1].LanguageName.ShouldBe("Spanish");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoLanguagesExist()
        {
            // Arrange
            _languageRepository.Setup(x => x.GetAllAsync(false)).ReturnsAsync(new List<Language>());

            var query = new GetLanguagesQuery();
            var handler = new GetLanguagesQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Languages.NotFound");
        }
    }
}
