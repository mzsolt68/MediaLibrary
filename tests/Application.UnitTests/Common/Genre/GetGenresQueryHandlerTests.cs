using Application.Abstractions.Data;
using Application.Common;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace Application.UnitTests.Common
{
    public class GetGenresQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IGenreRepository> _genreRepository;

        public GetGenresQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _genreRepository = new Mock<IGenreRepository>();
            _unitOfWork.Setup(x => x.GenreRepository).Returns(_genreRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfGenreDTOs_WhenGenresExist()
        {
            // Arrange
            var genres = new List<Genre>
            {
                Genre.Create("Genre1", "Type1").Value,
                Genre.Create("Genre2", "Type2").Value
            };
            _genreRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Genre, bool>>>(), CancellationToken.None)).ReturnsAsync(genres);

            // Provide a valid predicate for the query
            Expression<Func<Genre, bool>> predicate = genre => true; 
            var query = new GetGenresQuery<Genre>(predicate);
            var handler = new GetGenresQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.GetType().ShouldBe(typeof(List<GenreDTO>));
            result.Value[0].GenreName.ShouldBe("Genre1");
            result.Value[1].GenreName.ShouldBe("Genre2");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoGenresExist()
        {
            // Arrange
            _genreRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Genre, bool>>>(), CancellationToken.None)).ReturnsAsync(new List<Genre>());

            // Provide a valid predicate for the query
            Expression<Func<Genre, bool>> predicate = genre => true; 
            var query = new GetGenresQuery<Genre>(predicate);
            var handler = new GetGenresQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Genres.NotFound");
        }
    }
}
