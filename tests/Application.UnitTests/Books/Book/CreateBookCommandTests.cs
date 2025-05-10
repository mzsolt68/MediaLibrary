using Application.Abstractions.Data;
using Application.Books;
using Domain.Models.Books;
using Domain.Models.Common;
using Moq;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Author _author = Author.Create("LastName", "FirstName", "").Value;
        private readonly BookFormat _bookFormat = BookFormat.Create("Hardcover").Value;
        private readonly Tag _tag = Tag.Create("TagName").Value;

        public CreateBookCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.BookRepository).Returns(new Mock<IBookRepository>().Object);
            _unitOfWork.Setup(x => x.AuthorRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_author);
            _unitOfWork.Setup(x => x.BookFormatRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_bookFormat);
            _unitOfWork.Setup(x => x.TagRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_tag);
        }

        [Fact]
        public async Task ProperParametersShouldCreateNewBook()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                BookTitle = "Sample Book",
                Edition = "1st",
                PublisherID = Guid.NewGuid(),
                PublishYear = "2025",
                ISBN = "1234567890",
                LanguageID = Guid.NewGuid(),
                AuthorIDs = new List<Guid> { _author.Id },
                FormatIDs = new List<Guid> { _bookFormat.Id },
                TagIDs = new List<Guid> { _tag.Id }
            };

            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new CreateBookCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task MissingBookTitleShouldReturnFailure()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                BookTitle = "",
                Edition = "1st",
                PublisherID = Guid.NewGuid(),
                PublishYear = "2025",
                ISBN = "1234567890",
                LanguageID = Guid.NewGuid(),
                AuthorIDs = new List<Guid> { Guid.NewGuid() },
                FormatIDs = new List<Guid> { Guid.NewGuid() },
                TagIDs = new List<Guid> { Guid.NewGuid() }
            };
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            // Act
            var handler = new CreateBookCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("BookTitle.Empty");
        }
    }
}