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
        private HashSet<AuthorBook> _authors;
        private HashSet<FormatBook> _formats;
        private HashSet<TagBook> _tags;

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
            _authors = new HashSet<AuthorBook>();
            _formats = new HashSet<FormatBook>();
            _tags = new HashSet<TagBook>();
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
        public virtual ICollection<AuthorBook> Authors => _authors.ToList();

        /// <summary>
        /// Gets the collection of formats associated with the book.
        /// </summary>
        public virtual ICollection<FormatBook> Formats => _formats.ToList();

        /// <summary>
        /// Gets the collection of tags associated with the book.
        /// </summary>
        public virtual ICollection<TagBook> Tags => _tags.ToList();

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
            var book = new Book(Guid.NewGuid(), bookTitle, edition, publisherID, publishYear, isbn, languageID);
            book.IsActive = true;
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
        /// <param name="authorID">The unique identifier of the author to add.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="AuthorBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<AuthorBook> AddAuthor(Guid authorID)
        {
            if (authorID == Guid.Empty)
            {
                return Result.Failure<AuthorBook>(new Error("AuthorID.Empty", "AuthorID is required.", ErrorType.Validation));
            }
            if (_authors.Any(ab => ab.AuthorID == authorID))
            {
                return Result.Failure<AuthorBook>(new Error("Author.AlreadyAdded", "Author is already added to the book", ErrorType.Failure));
            }
            var authorBook = AuthorBook.Create(authorID, Id);
            _authors.Add(authorBook.Value);
            return Result.Success(authorBook.Value);
        }

        /// <summary>
        /// Removes an author from the book.
        /// </summary>
        /// <param name="authorID">The unique identifier of the author to remove.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed <see cref="AuthorBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<AuthorBook> RemoveAuthor(Guid authorID)
        {
            if (authorID == Guid.Empty)
            {
                return Result.Failure<AuthorBook>(new Error("Author.Empty", "Author cannot be empty.", ErrorType.Validation));
            }
            var authorBook = _authors.SingleOrDefault(ab => ab.AuthorID == authorID);
            if (authorBook == null)
            {
                return Result.Failure<AuthorBook>(new Error("Author.NotFound", "Author is not added to the book", ErrorType.NotFound));
            }
            _authors.Remove(authorBook);
            return Result.Success(authorBook);
        }

        /// <summary>
        /// Adds a format to the book.
        /// </summary>
        /// <param name="formatID">The unique identifier of the format to add.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="FormatBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<FormatBook> AddFormat(Guid formatID)
        {
            if (formatID == Guid.Empty)
            {
                return Result.Failure<FormatBook>(new Error("FormatID.Missing", "FormatID is required.", ErrorType.Validation));
            }
            if (_formats.Any(fb => fb.FormatID == formatID))
            {
                return Result.Failure<FormatBook>(new Error("Format.AlreadyAdded", "Format is already added to the book", ErrorType.Failure));
            }
            var formatBook = FormatBook.Create(formatID, Id);
            _formats.Add(formatBook.Value);
            return Result.Success(formatBook.Value);
        }

        /// <summary>
        /// Removes a format from the book.
        /// </summary>
        /// <param name="formatID">The unique identifier of the format to remove.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed <see cref="FormatBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<FormatBook> RemoveFormat(Guid formatID)
        {
            if (formatID == Guid.Empty)
            {
                return Result.Failure<FormatBook>(new Error("FormatID.Missing", "FormatID is required", ErrorType.Validation));
            }
            var formatBook = _formats.SingleOrDefault(fb => fb.FormatID == formatID);
            if (formatBook == null)
            {
                return Result.Failure<FormatBook>(new Error("Format.NotFound", "Format is not added to the book", ErrorType.NotFound));
            }
            _formats.Remove(formatBook);
            return Result.Success(formatBook);
        }

        /// <summary>
        /// Adds a tag to the book.
        /// </summary>
        /// <param name="tagID">The unique identifier of the tag to add.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created <see cref="TagBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<TagBook> AddTag(Guid tagID)
        {
            if (tagID == Guid.Empty)
            {
                return Result.Failure<TagBook>(new Error("TagID.Missing", "TagID is required", ErrorType.Validation));
            }
            if (_tags.Any(tb => tb.TagID == tagID))
            {
                return Result.Failure<TagBook>(new Error("Tag.AlreadyAdded", "Tag is already added to the book", ErrorType.Failure));
            }
            var tagBook = TagBook.Create(Id, tagID);
            _tags.Add(tagBook.Value);
            return Result.Success(tagBook.Value);
        }

        /// <summary>
        /// Removes a tag from the book.
        /// </summary>
        /// <param name="tagID">The unique identifier of the tag to remove.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed <see cref="TagBook"/> instance if successful, or an error if validation fails.</returns>
        public Result<TagBook> RemoveTag(Guid tagID)
        {
            if (tagID == Guid.Empty)
            {
                return Result.Failure<TagBook>(new Error("TagID.Missing", "TagID is required", ErrorType.Validation));
            }
            var tagBook = _tags.SingleOrDefault(tb => tb.TagID == tagID);
            if (tagBook == null)
            {
                return Result.Failure<TagBook>(new Error("Tag.NotFound", "Tag is not added to the book", ErrorType.NotFound));
            }
            _tags.Remove(tagBook);
            return Result.Success(tagBook);
        }
    }
}
