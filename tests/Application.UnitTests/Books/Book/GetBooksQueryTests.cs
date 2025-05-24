using Application.Abstractions.Data;
using Application.Books;
using Application.Dto;
using Application.Dto.Books;
using Domain.Models.Books;
using MockQueryable;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetBooksQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly SearchParamsDTO _searchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = []
        };

        public GetBooksQueryTests()
        {
            _unitOfWork = new Mock<IUnitOfWork> (); ;
            _bookRepository = new Mock<IBookRepository> ();
            _unitOfWork.Setup(x => x.BookRepository).Returns(_bookRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfBookDTOs_NoSearchParams()
        {
            // Arrange
            var books = new List<Book>
            {
                Book.Create("Title1", "", Guid.NewGuid(), "", "", Guid.NewGuid()).Value,
                Book.Create("Title2", "", Guid.NewGuid(), "", "", Guid.NewGuid()).Value,
                Book.Create("Title3", "", Guid.NewGuid(), "", "", Guid.NewGuid()).Value
            };
            foreach (var book in books)
            {
                book.AddAuthor(Author.Create("Lastname", "Firstname", "").Value);
            }

            _bookRepository.Setup(x => x.GetAll()).Returns(books.BuildMock());

            var query = new GetBooksQuery(_searchParams);
            var handler = new GetBooksQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(3);
            result.Value[0].ShouldBeOfType<BookDTO>();
            result.Value[1].ShouldBeOfType<BookDTO>();
            result.Value[2].ShouldBeOfType<BookDTO>();
            result.Value[0].BookTitle.ShouldBe("Title1");
            result.Value[1].BookTitle.ShouldBe("Title2");
            result.Value[2].BookTitle.ShouldBe("Title3");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoBooksExist()
        {
            var emptyBooks = new List<Book>().AsQueryable().BuildMock();
            // Arrange
            _bookRepository.Setup(x => x.GetAll()).Returns(emptyBooks);

            var query = new GetBooksQuery(_searchParams);
            var handler = new GetBooksQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Books.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

    }
}
