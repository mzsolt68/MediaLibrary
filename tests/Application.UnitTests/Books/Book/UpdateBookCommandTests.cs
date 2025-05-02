using Application.Abstractions.Data;
using Application.Books;
using Domain.Models.Books;
using Moq;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class UpdateBookCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdateBookCommandTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.BookRepository).Returns(new Mock<IBookRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldUpdateBook()
        {
            // Arrange
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var book = Domain.Models.Books.Book.Create("Sample Book", "1st", Guid.NewGuid(), "2025", "1234567890", Guid.NewGuid()).Value;
            _unitOfWork.Setup(x => x.BookRepository.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(book);
            var bookId = book.Id;
            var command = new UpdateBookCommand
            {
                BookID = bookId,
                BookTitle = "Updated Book",
                Edition = "2nd",
                PublisherID = Guid.NewGuid(),
                PublishYear = "2026",
                ISBN = "0987654321",
                LanguageID = Guid.NewGuid(),
                AuthorIDs = new List<Guid> { Guid.NewGuid() },
                FormatIDs = new List<Guid> { Guid.NewGuid() },
                TagIDs = new List<Guid> { Guid.NewGuid() }
            };

            // Act
            var handler = new UpdateBookCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task NonExistentBookShouldReturnNotFound()
        {
            // Arrange
            _unitOfWork.Setup(x => x.BookRepository.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(null as Domain.Models.Books.Book);
            var command = new UpdateBookCommand
            {
                BookID = Guid.NewGuid(),
                BookTitle = "Updated Book",
                Edition = "2nd",
                PublisherID = Guid.NewGuid(),
                PublishYear = "2026",
                ISBN = "0987654321",
                LanguageID = Guid.NewGuid(),
                AuthorIDs = new List<Guid> { Guid.NewGuid() },
                FormatIDs = new List<Guid> { Guid.NewGuid() },
                TagIDs = new List<Guid> { Guid.NewGuid() }
            };
            // Act
            var handler = new UpdateBookCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
        }
    }
}