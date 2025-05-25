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
    public class GetBookWithDetailsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookRepository> _bookRepository;

        public GetBookWithDetailsQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork.Setup(x => x.BookRepository).Returns(_bookRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturn_Failure_WhenBook_DoesNotExist()
        {
            var bookId = Guid.NewGuid();
            _bookRepository.Setup(b => b.GetBookWithFullDataAsync(bookId, CancellationToken.None)).ReturnsAsync((Book?)null);

            var query = new GetBookWithDetailsQuery(bookId);
            var handler = new GetBookWithDetailsQueryHandler(_unitOfWork.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Book.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturn_BookDetailsDTO_WhenBookID_Valid()
        {
            var publisher = Publisher.Create("Publisher").Value;
            var language = Language.Create("Magyar").Value;
            var formats = new List<BookFormat>
            {
                BookFormat.Create("PDF").Value,
                BookFormat.Create("mobi").Value
            };
            var tags = new List<Tag>
            {
                Tag.Create("Programing").Value,
                Tag.Create(".NET").Value
            };
            var book = Book.Create("Title", "2nd", publisher.Id, "2000", "978 963 05 8746 4", language.Id).Value;
            foreach(var format in formats)
            {
                book.AddFormat(format);
            }
            foreach(var tag in tags)
            {
                book.AddTag(tag);
            }
            book.AddAuthor(Author.Create("Lastname", "Firstname", "").Value);
            book.SetPublisher(publisher);
            book.SetLangugage(language);

            _bookRepository.Setup(x => x.GetBookWithFullDataAsync(book.Id, CancellationToken.None)).ReturnsAsync(book);

            var query = new GetBookWithDetailsQuery(book.Id);
            var handler = new GetBookWithDetailsQueryHandler(_unitOfWork.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBeOfType<BookDetailsDTO>();
            result.Value.Book.BookTitle.ShouldBe(book.BookTitle);
            result.Value.Language.LanguageName.ShouldBe(language.LanguageName);
            result.Value.Publisher.PublisherName.ShouldBe(publisher.PublisherName);
            result.Value.ISBN.ShouldBe(book.ISBN);
            result.Value.Formats.Count.ShouldBe(formats.Count);
            result.Value.Tags.Count.ShouldBe(tags.Count);
        }
    }
}
