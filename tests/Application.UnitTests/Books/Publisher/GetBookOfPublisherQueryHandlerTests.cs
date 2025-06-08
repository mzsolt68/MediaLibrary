using Application.Abstractions.Data;
using Application.Books;
using Application.Dto.Books;
using Domain.Models.Books;
using Domain.Models.Common;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetBookOfPublisherQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IPublisherRepository> _publisherRepository;

        public GetBookOfPublisherQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _publisherRepository = new Mock<IPublisherRepository>();
            _unitOfWork.Setup(x => x.PublisherRepository).Returns(_publisherRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPublisherWithBooks_WhenPublisherExists()
        {
            // Arrange
            var publisher = Publisher.Create("Test Publisher").Value;
            var language = Language.Create("English").Value;
            var books = new List<Book>
            {
                Book.Create("Book1", "1st", publisher.Id, "2021", "ISBN1", language.Id).Value,
                Book.Create("Book2", "2nd", publisher.Id, "2022", "ISBN2", language.Id).Value,
                Book.Create("Book3", "1st", publisher.Id, "2023", "ISBN3", language.Id).Value
            };

            foreach (var book in books)
            {
                book.SetPublisher(publisher);
                book.SetLangugage(language);
                book.AddAuthor(Author.Create("Lastname", "Firstname", "").Value);
            }

            _publisherRepository.Setup(x => x.GetByIdAsync(publisher.Id, CancellationToken.None))
                .ReturnsAsync(publisher);

            _publisherRepository.Setup(x => x.GetPublishersBooksAsync(publisher.Id, CancellationToken.None))
                .ReturnsAsync(books);

            var query = new GetBooksOfPublisherQuery(publisher.Id);
            var handler = new GetBooksOfPublisherQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeOfType<BookPublisherDetailsDTO>();
            result.Value.Publisher.ShouldNotBeNull();
            result.Value.Publisher.PublisherName.ShouldBe("Test Publisher");
            result.Value.Publisher.PublisherID.ShouldBe(publisher.Id);
            result.Value.Books.Count.ShouldBe(3);
            result.Value.Books.First().BookTitle.ShouldBe("Book1");
            result.Value.Books.Skip(1).First().BookTitle.ShouldBe("Book2");
            result.Value.Books.Last().BookTitle.ShouldBe("Book3");
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenPublisherDoesNotExist()
        {
            // Arrange
            var publisherId = Guid.NewGuid();
            _publisherRepository.Setup(x => x.GetByIdAsync(publisherId, CancellationToken.None))
                .ReturnsAsync((Publisher?)null);

            var query = new GetBooksOfPublisherQuery(publisherId);
            var handler = new GetBooksOfPublisherQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.NotFound);
            result.Error.Code.ShouldBe("Publisher.NotFound");
        }

        [Fact]
        public async Task Handle_ShouldReturnPublisherWithEmptyBooks_WhenPublisherHasNoBooks()
        {
            // Arrange
            var publisher = Publisher.Create("Test Publisher").Value;

            _publisherRepository.Setup(x => x.GetByIdAsync(publisher.Id, CancellationToken.None))
                .ReturnsAsync(publisher);

            _publisherRepository.Setup(x => x.GetPublishersBooksAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(new List<Book>());

            var query = new GetBooksOfPublisherQuery(publisher.Id);
            var handler = new GetBooksOfPublisherQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
            result.Value.Publisher.ShouldNotBeNull();
            result.Value.Publisher.PublisherName.ShouldBe("Test Publisher");
            result.Value.Books.Count.ShouldBe(0);
        }
    }
}
