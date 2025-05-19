using Application.Abstractions.Data;
using Application.Common;
using Application.Dto;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;
using System.Linq.Expressions;
using MockQueryable;
using SharedKernel;

namespace Application.UnitTests.Common
{
    public class GetGenresQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IGenreRepository> _genreRepository;
        private readonly SearchParamsDTO _searchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = []
        };

        public GetGenresQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _genreRepository = new Mock<IGenreRepository>();
            _unitOfWork.Setup(x => x.GenreRepository).Returns(_genreRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfGenreDTOs_WhenGenresExist_NoSearchParams()
        {
            // Arrange
            var genres = new List<Genre>
            {
                Genre.Create("Genre1", "Type1").Value,
                Genre.Create("Genre2", "Type2").Value
            }.BuildMock();

            _genreRepository.Setup(x => x.GetAll()).Returns(genres);

            var query = new GetGenresQuery() { SearchParams = _searchParams};
            var handler = new GetGenresQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value[0].GenreName.ShouldBe("Genre1");
            result.Value[1].GenreName.ShouldBe("Genre2");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoGenresExist()
        {
            var emptyGenres = new List<Genre>().AsQueryable().BuildMock();
            // Arrange
            _genreRepository.Setup(x => x.GetAll()).Returns(emptyGenres);

            var query = new GetGenresQuery() { SearchParams = _searchParams };
            var handler = new GetGenresQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Genres.NotFound");
        }
    }
}
