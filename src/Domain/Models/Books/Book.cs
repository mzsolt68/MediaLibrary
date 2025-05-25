using Domain.Models.Common;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    /// <summary>
    /// Represents a book entity in the domain.
    /// </summary>
    public class Book : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class with the specified ID.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private Book(Guid id) : base(id) { }

        private readonly HashSet<Author> _authors;
        private readonly HashSet<BookFormat> _formats;
        private readonly HashSet<Tag> _tags;

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the book.</param>
        /// <param name="bookTitle">The title of the book.</param>
        /// <param name="edition">The edition of the book.</param>
        /// <param name="publisherID">The unique identifier of the publisher.</param>
        /// <param name="publishYear">The year the book was published.</param>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <param name="languageID">The unique identifier of the language.</param>
        private Book(Guid id, string bookTitle, string edition, Guid publisherID, string publishYear, string isbn, Guid languageID) : base(id)
        {
            BookTitle = bookTitle;
            Edition = edition;
            PublisherID = publisherID;
            PublishYear = publishYear;
            ISBN = isbn;
            LanguageID = languageID;
            _authors = [];
            _formats = [];
            _tags = [];
        }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        [Required]
        [Display(Name = "Könyv címe")]
        public string BookTitle { get; private set; }

        /// <summary>
        /// Gets the edition of the book.
        /// </summary>
        [Display(Name = "Kiadás")]
        public string Edition { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the publisher.
        /// </summary>
        public Guid PublisherID { get; private set; }

        /// <summary>
        /// Gets the publisher entity associated with the book.
        /// </summary>
        [Display(Name = "Kiadó")]
        public Publisher Publisher { get; private set; }

        /// <summary>
        /// Gets the year the book was published.
        /// </summary>
        [Display(Name = "Kiadás éve")]
        public string PublishYear { get; private set; }

        /// <summary>
        /// Gets the ISBN of the book.
        /// </summary>
        [Display(Name = "ISBN")]
        public string ISBN { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the language.
        /// </summary>
        public Guid LanguageID { get; private set; }

        /// <summary>
        /// Gets the language entity associated with the book.
        /// </summary>
        [Display(Name = "Nyelv")]
        public Language Language { get; private set; }

        /// <summary>
        /// Gets the collection of authors associated with the book.
        /// </summary>
        public virtual ICollection<Author> Authors => [.. _authors];

        /// <summary>
        /// Gets the collection of formats associated with the book.
        /// </summary>
        public virtual ICollection<BookFormat> Formats => [.. _formats];

        /// <summary>
        /// Gets the collection of tags associated with the book.
        /// </summary>
        public virtual ICollection<Tag> Tags => [.. _tags];

        /// <summary>
        /// Creates a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="bookTitle">The title of the book.</param>
        /// <param name="edition">The edition of the book.</param>
        /// <param name="publisherID">The unique identifier of the publisher.</param>
        /// <param name="publishYear">The year the book was published.</param>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <param name="languageID">The unique identifier of the language.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="Book"/> instance if successful, or an error if validation fails.</returns>
        public static Result<Book> Create(string bookTitle, string edition, Guid publisherID, string publishYear, string isbn, Guid languageID)
        {
            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                return Result.Failure<Book>(new Error("BookTitle.Empty", "BookTitle cannot be empty.", ErrorType.Validation));
            }
            var book = new Book(Guid.NewGuid(), bookTitle, edition, publisherID, publishYear, isbn, languageID)
            {
                IsActive = true
            };
            return Result.Success(book);
        }

        /// <summary>
        /// Updates the properties of the book.
        /// </summary>
        /// <param name="bookTitle">The new title of the book.</param>
        /// <param name="edition">The new edition of the book.</param>
        /// <param name="publisherID">The new unique identifier of the publisher.</param>
        /// <param name="publishYear">The new year the book was published.</param>
        /// <param name="isbn">The new ISBN of the book.</param>
        /// <param name="languageID">The new unique identifier of the language.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure of the update operation.</returns>
        public Result Update(string bookTitle, string edition, Guid publisherID, string publishYear, string isbn, Guid languageID)
        {
            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                return Result.Failure(new Error("BookTitle.Empty", "BookTitle cannot be empty.", ErrorType.Validation));
            }
            BookTitle = bookTitle;
            Edition = edition;
            PublisherID = publisherID;
            PublishYear = publishYear;
            ISBN = isbn;
            LanguageID = languageID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Adds an author to the book.
        /// </summary>
        /// <param name="author">The author to add.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="AuthorBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<AuthorBook> AddAuthor(Author author)
        {
            if (_authors.Any(ab => ab.Id == author.Id))
            {
                return Result.Failure<AuthorBook>(new Error("Author.AlreadyAdded", "Author is already added to the book", ErrorType.Failure));
            }
            var authorBook = AuthorBook.Create(author.Id, Id);
            _authors.Add(author);
            return Result.Success(authorBook.Value);
        }

        /// <summary>
        /// Removes an author from the book.
        /// </summary>
        /// <param name="author">The author to remove.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed <see cref="Author"/> instance if successful, or an error if validation fails.</returns>
        public Result<Author> RemoveAuthor(Author author)
        {
            var authorToRemove = _authors.SingleOrDefault(ab => ab.Id == author.Id);
            if (authorToRemove == null)
            {
                return Result.Failure<Author>(new Error("Author.NotFound", "Author is not added to the book", ErrorType.NotFound));
            }
            _authors.Remove(authorToRemove);
            return Result.Success(authorToRemove);
        }

        /// <summary>
        /// Adds a format to the book.
        /// </summary>
        /// <param name="format">The format to add.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="FormatBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<FormatBook> AddFormat(BookFormat format)
        {
            if (_formats.Any(fb => fb.Id == format.Id))
            {
                return Result.Failure<FormatBook>(new Error("Format.AlreadyAdded", "Format is already added to the book", ErrorType.Failure));
            }
            var formatBook = FormatBook.Create(format.Id, Id);
            _formats.Add(format);
            return Result.Success(formatBook.Value);
        }

        /// <summary>
        /// Removes a format from the book.
        /// </summary>
        /// <param name="format">The format to remove.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed <see cref="BookFormat"/> instance if successful, or an error if validation fails.</returns>
        public Result<BookFormat> RemoveFormat(BookFormat format)
        {
            var formatToRemove = _formats.SingleOrDefault(fb => fb.Id == format.Id);
            if (formatToRemove == null)
            {
                return Result.Failure<BookFormat>(new Error("Format.NotFound", "Format is not added to the book", ErrorType.NotFound));
            }
            _formats.Remove(formatToRemove);
            return Result.Success(formatToRemove);
        }

        /// <summary>
        /// Adds a tag to the book.
        /// </summary>
        /// <param name="tag">The tag to add.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="TagBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<TagBook> AddTag(Tag tag)
        {
            if (_tags.Any(tb => tb.Id == tag.Id))
            {
                return Result.Failure<TagBook>(new Error("Tag.AlreadyAdded", "Tag is already added to the book", ErrorType.Failure));
            }
            var tagBook = TagBook.Create(Id, tag.Id);
            _tags.Add(tag);
            return Result.Success(tagBook.Value);
        }

        /// <summary>
        /// Removes a tag from the book.
        /// </summary>
        /// <param name="tag">The tag to remove.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed <see cref="Tag"/> instance if successful, or an error if validation fails.</returns>
        public Result<Tag> RemoveTag(Tag tag)
        {
            var tagToRemove = _tags.SingleOrDefault(tb => tb.Id == tag.Id);
            if (tagToRemove == null)
            {
                return Result.Failure<Tag>(new Error("Tag.NotFound", "Tag is not added to the book", ErrorType.NotFound));
            }
            _tags.Remove(tagToRemove);
            return Result.Success(tagToRemove);
        }

        public Result SetLangugage(Language language)
        {
            Language = language;
            LanguageID = language.Id;
            return Result.Success();
        }

        public Result setPublisher(Publisher publisher)
        { 
            Publisher = publisher;
            PublisherID = publisher.Id;
            return Result.Success();
        }
    }
}
