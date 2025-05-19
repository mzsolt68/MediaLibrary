using Application.Abstractions.Data;
using Application.Common;
using Application.Dto;
using Application.Dto.Common;
using Domain.Models.Common;
using MockQueryable;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace Application.UnitTests.Common
{
    public class GetLanguagesQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILanguageRepository> _languageRepository;
        private readonly SearchParamsDTO _searchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = []
        };

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
            }.BuildMock();
            _languageRepository.Setup(x => x.GetAll()).Returns(languages);

            var query = new GetLanguagesQuery(_searchParams);
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
            var emptyLanguages = new List<Language>().BuildMock();
            _languageRepository.Setup(x => x.GetAll()).Returns(emptyLanguages);

            var query = new GetLanguagesQuery(_searchParams);
            var handler = new GetLanguagesQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Languages.NotFound");
        }
    }
}
