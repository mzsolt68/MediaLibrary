using Shouldly;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit tests for the <see cref="Book"/> entity.
    /// </summary>
    public class BookTests
    {
        /// <summary>
        /// Proper parameters should create a new <see cref="Book"/> instance.
        /// </summary>
        [Fact]
        public void ProperParametersShouldCreateNewInstance()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            // Act
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID);
            // Assert
            book.ShouldNotBeNull();
            book.IsSuccess.ShouldBeTrue();
            book.IsFailure.ShouldBeFalse();
            book.Value.BookTitle.ShouldBe(bookTitle);
            book.Value.Edition.ShouldBe(edition);
            book.Value.PublisherID.ShouldBe(publisherID);
            book.Value.PublishYear.ShouldBe(publishYear);
            book.Value.ISBN.ShouldBe(isbn);
            book.Value.LanguageID.ShouldBe(languageID);
            book.Value.IsActive.ShouldBeTrue();
            book.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            book.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            book.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty book title should return a failure.
        /// <see cref="Book.Create(string, string, Guid, string, string, Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyBookTitleShouldReturnFailure()
        {
            // Arrange
            var bookTitle = string.Empty;
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            // Act
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID);
            // Assert
            book.ShouldNotBeNull();
            book.IsFailure.ShouldBeTrue();
            book.IsSuccess.ShouldBeFalse();
            book.Error.Code.ShouldBe("BookTitle.Empty");
            book.Error.Message.ShouldBe("BookTitle cannot be empty.");
            book.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Proper parameters should add a new author to the book.
        /// <see cref="Book.AddAuthor(Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShouldAddNewAuthorToBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var authorID = Guid.NewGuid();
            // Act
            var authorBook = book.AddAuthor(authorID);
            // Assert
            authorBook.ShouldNotBeNull();
            authorBook.IsSuccess.ShouldBeTrue();
            authorBook.IsFailure.ShouldBeFalse();
            authorBook.Value.BookID.ShouldBe(book.Id);
            authorBook.Value.AuthorID.ShouldBe(authorID);
            authorBook.Value.IsActive.ShouldBeTrue();
            authorBook.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            authorBook.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            authorBook.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty author ID should return a failure when adding.
        /// <see cref="Book.AddAuthor(Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyAuthorIDShouldReturnFailureWhenAdding()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var authorID = Guid.Empty;
            // Act
            var authorBook = book.AddAuthor(authorID);
            // Assert
            authorBook.ShouldNotBeNull();
            authorBook.IsFailure.ShouldBeTrue();
            authorBook.IsSuccess.ShouldBeFalse();
            authorBook.Error.Code.ShouldBe("AuthorID.Empty");
            authorBook.Error.Message.ShouldBe("AuthorID is required.");
            authorBook.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Proper parameters should remove an author from the book.
        /// <see cref="Book.RemoveAuthor(Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShouldRemoveAuthorFromBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var authorID = Guid.NewGuid();
            var authorBook = book.AddAuthor(authorID).Value;
            // Act
            var result = book.RemoveAuthor(authorID);
            // Assert
            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(authorBook);
        }

        /// <summary>
        /// Empty author ID should return a failure when removing.
        /// <see cref="Book.RemoveAuthor(Guid)"/>
        /// </summary>  
        [Fact]
        public void EmptyAuthorIDShouldReturnFailureWhenRemoving()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var authorID = Guid.Empty;
            // Act
            var result = book.RemoveAuthor(authorID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Author.Empty");
            result.Error.Message.ShouldBe("Author cannot be empty.");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Already added author ID should return a failure when adding.
        /// <see cref="Book.AddAuthor(Guid)"/>
        /// </summary>  
        [Fact]
        public void AlreadyAddedAuthorIDShouldReturnFailureWhenAdding()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var authorID = Guid.NewGuid();
            book.AddAuthor(authorID);
            // Act
            var result = book.AddAuthor(authorID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Author.AlreadyAdded");
            result.Error.Message.ShouldBe("Author is already added to the book");
            result.Error.Type.ShouldBe(ErrorType.Failure);
        }

        /// <summary>
        /// Not added author ID should return a failure when removing.
        /// <see cref="Book.RemoveAuthor(Guid)"/>
        /// </summary>  
        [Fact]
        public void NotAddedAuthorIDShouldReturnFailureWhenRemoving()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var authorID = Guid.NewGuid();
            // Act
            var result = book.RemoveAuthor(authorID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Author.NotFound");
            result.Error.Message.ShouldBe("Author is not added to the book");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        /// <summary>
        /// Proper parameters should add a new format to the book.
        /// <see cref="Book.AddFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShouldAddNewFormatToBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var formatID = Guid.NewGuid();
            // Act
            var formatBook = book.AddFormat(formatID);
            // Assert
            formatBook.ShouldNotBeNull();
            formatBook.IsSuccess.ShouldBeTrue();
            formatBook.IsFailure.ShouldBeFalse();
            formatBook.Value.BookID.ShouldBe(book.Id);
            formatBook.Value.FormatID.ShouldBe(formatID);
            formatBook.Value.IsActive.ShouldBeTrue();
            formatBook.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            formatBook.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            formatBook.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty format ID should return a failure when adding.
        /// <see cref="Book.AddFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyFormatIDShouldReturnFailureWhenAdding()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var formatID = Guid.Empty;
            // Act
            var formatBook = book.AddFormat(formatID);
            // Assert
            formatBook.ShouldNotBeNull();
            formatBook.IsFailure.ShouldBeTrue();
            formatBook.IsSuccess.ShouldBeFalse();
            formatBook.Error.Code.ShouldBe("FormatID.Missing");
            formatBook.Error.Message.ShouldBe("FormatID is required.");
            formatBook.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Already added format ID should return a failure when adding.
        /// <see cref="Book.AddFormat(Guid)"/>
        /// </summary>  
        [Fact]
        public void AlreadyAddedFormatIDShouldReturnFailureWhenAdding()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var formatID = Guid.NewGuid();
            book.AddFormat(formatID);
            // Act
            var result = book.AddFormat(formatID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Format.AlreadyAdded");
            result.Error.Message.ShouldBe("Format is already added to the book");
            result.Error.Type.ShouldBe(ErrorType.Failure);
        }

        /// <summary>
        /// Proper parameters should remove a format from the book.
        /// <see cref="Book.RemoveFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShouldRemoveFormatFromBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var formatID = Guid.NewGuid();
            var formatBook = book.AddFormat(formatID).Value;
            // Act
            var result = book.RemoveFormat(formatID);
            // Assert
            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(formatBook);
        }

        /// <summary>
        /// Empty format ID should return a failure when removing.
        /// <see cref="Book.RemoveFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyFormatIDShouldReturnFailureWhenRemoving()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var formatID = Guid.Empty;
            // Act
            var result = book.RemoveFormat(formatID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("FormatID.Missing");
            result.Error.Message.ShouldBe("FormatID is required");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Not added format ID should return a failure when removing.
        /// <see cref="Book.RemoveFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void NotAddedFormatIDShouldReturnFailureWhenRemoving()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var formatID = Guid.NewGuid();
            // Act
            var result = book.RemoveFormat(formatID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Format.NotFound");
            result.Error.Message.ShouldBe("Format is not added to the book");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        /// <summary>
        /// Proper parameter should add a new tag to the book.
        /// <see cref="Book.AddTag(Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParameterShouldAddNewTagToBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var tag = Guid.NewGuid();
            // Act
            var result = book.AddTag(tag);
            // Assert
            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.BookID.ShouldBe(book.Id);
            result.Value.TagID.ShouldBe(tag);
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty tag should return a failure when adding.
        /// <see cref="Book.AddTag(Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyTagShouldReturnFailureWhenAdding()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var tag = Guid.Empty;
            // Act
            var result = book.AddTag(tag);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("TagID.Missing");
            result.Error.Message.ShouldBe("TagID is required");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Already added tag should return a failure when adding.
        /// <see cref="Book.AddTag(Guid)"/>
        /// </summary>
        [Fact]
        public void AlreadyAddedTagShouldReturnFailureWhenAdding()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var tag = Guid.NewGuid();
            book.AddTag(tag);
            // Act
            var result = book.AddTag(tag);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Tag.AlreadyAdded");
            result.Error.Message.ShouldBe("Tag is already added to the book");
            result.Error.Type.ShouldBe(ErrorType.Failure);
        }

        /// <summary>
        /// Proper parameter should remove a tag from the book.
        /// <see cref="Book.RemoveTag(Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParameterShouldRemoveTagFromBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var tag = Guid.NewGuid();
            var tagBook = book.AddTag(tag).Value;
            // Act
            var result = book.RemoveTag(tag);
            // Assert
            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(tagBook);
        }

        /// <summary>
        /// Empty tag should return a failure when removing.
        /// <see cref="Book.RemoveTag(Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyTagShouldReturnFailureWhenRemoving()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var tag = Guid.Empty;
            // Act
            var result = book.RemoveTag(tag);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("TagID.Missing");
            result.Error.Message.ShouldBe("TagID is required");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }

        /// <summary>
        /// Not added tag should return a failure when removing.
        /// <see cref="Book.RemoveTag(Guid)"/>
        /// </summary>
        [Fact]
        public void NotAddedTagShouldReturnFailureWhenRemoving()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            var tag = Guid.NewGuid();
            // Act
            var result = book.RemoveTag(tag);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("Tag.NotFound");
            result.Error.Message.ShouldBe("Tag is not added to the book");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        /// <summary>
        /// Proper parameters should update the book.
        /// <see cref="Book.Update(string, string, Guid, string, string, Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShouldUpdateBook()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            // Act
            var result = book.Update("New Book Title", "2nd", Guid.NewGuid(), "2022", "978-3-16-148410-1", Guid.NewGuid());
            // Assert
            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            book.BookTitle.ShouldBe("New Book Title");
            book.Edition.ShouldBe("2nd");
            book.PublisherID.ShouldNotBe(publisherID);
            book.PublishYear.ShouldBe("2022");
            book.ISBN.ShouldBe("978-3-16-148410-1");
        }

        /// <summary>
        /// Missing title should return a failure when updating.
        /// <see cref="Book.Update(string, string, Guid, string, string, Guid)"/>
        /// </summary>
        [Fact]
        public void MissingTitleShouldReturnFailureWhenUpdating()
        {
            // Arrange
            var bookTitle = "Book Title";
            var edition = "1st";
            var publisherID = Guid.NewGuid();
            var publishYear = "2021";
            var isbn = "978-3-16-148410-0";
            var languageID = Guid.NewGuid();
            var book = Book.Create(bookTitle, edition, publisherID, publishYear, isbn, languageID).Value;
            // Act
            var result = book.Update(string.Empty, edition, publisherID, publishYear, isbn, languageID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("BookTitle.Empty");
            result.Error.Message.ShouldBe("BookTitle cannot be empty.");
            result.Error.Type.ShouldBe(ErrorType.Validation);
        }
    }
}
