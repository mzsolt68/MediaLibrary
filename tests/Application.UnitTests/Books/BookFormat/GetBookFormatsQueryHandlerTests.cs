using Shouldly;
using Moq;
using Application.Abstractions.Data;
using Application.Books;
using Application.Dto;
using Domain.Models.Books;
using MockQueryable.Moq;
using SharedKernel;
using System.Linq.Expressions;

namespace Application.UnitTests.Books
{
    public class GetBookFormatsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookFormatRepository> _bookFormatRepository;
        private readonly SearchParamsDTO _defaultSearchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = new List<SearchParam>()
        };

        public GetBookFormatsQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _bookFormatRepository = new Mock<IBookFormatRepository>();
            _unitOfWork.Setup(x => x.BookFormatRepository).Returns(_bookFormatRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfBookFormatDTOs_WhenNoSearchParams()
        {
            // Arrange
            var formats = new List<BookFormat>
            {
                BookFormat.Create("Hardcover").Value,
                BookFormat.Create("Paperback").Value,
                BookFormat.Create("Ebook").Value
            };
            var mockQueryable = formats.AsQueryable().BuildMockDbSet().Object;
            _bookFormatRepository.Setup(x => x.GetAll()).Returns(mockQueryable);

            var query = new GetBookFormatsQuery(_defaultSearchParams);
            var handler = new GetBookFormatsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(3);
            result.Value[0].FormatName.ShouldBe("Hardcover");
            result.Value[1].FormatName.ShouldBe("Paperback");
            result.Value[2].FormatName.ShouldBe("Ebook");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoBookFormatsExist()
        {
            // Arrange
            var emptyFormats = new List<BookFormat>().AsQueryable().BuildMockDbSet().Object;
            _bookFormatRepository.Setup(x => x.GetAll()).Returns(emptyFormats);

            var query = new GetBookFormatsQuery(_defaultSearchParams);
            var handler = new GetBookFormatsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("BookFormats.NotFound");
        }

        [Fact]
        public async Task Handle_ShouldApplySearchParamsAndReturnFilteredResults()
        {
            // Arrange
            var formats = new List<BookFormat>
            {
                BookFormat.Create("Hardcover").Value,
                BookFormat.Create("Paperback").Value,
                BookFormat.Create("Ebook").Value
            };
            var searchParams = new SearchParamsDTO
            {
                PageNumber = 1,
                PageSize = 10,
                SearchParams = new List<SearchParam>
                {
                    new SearchParam { PropertyName = "BookFormatName", Value = "Hard", MatchType = SearchType.Contains }
                }
            };
            _bookFormatRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func< BookFormat, bool>>>()))
                .Returns((Expression<Func<BookFormat, bool>> filter) =>
                    formats.AsQueryable().Where(filter).BuildMockDbSet().Object);

            var query = new GetBookFormatsQuery(searchParams);
            var handler = new GetBookFormatsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Count.ShouldBe(1);
            result.Value[0].FormatName.ShouldBe("Hardcover");
        }
    }
}
