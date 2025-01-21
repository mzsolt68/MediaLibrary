using Domain.Models.Common;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    public class Book : Entity
    {
        private HashSet<AuthorBook> _authors;
        private HashSet<FormatBook> _formats;
        private HashSet<TagBook> _tags;

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
        [Required]
        [Display(Name = "Könyv címe")]
        public string BookTitle { get; private set; }
        [Display(Name = "Kiadás")]
        public string Edition { get; private set; }
        public Guid PublisherID { get; private set; }
        [Display(Name = "Kiadó")]
        public Publisher Publisher { get; private set; }
        [Display(Name = "Kiadás éve")]
        public string PublishYear { get; private set; }
        [Display(Name = "ISBN")]
        public string ISBN { get; private set; }
        public Guid LanguageID { get; private set; }
        [Display(Name = "Nyelv")]
        public Language Language { get; private set; }

        public virtual ICollection<AuthorBook> Authors => _authors.ToList();
        public virtual ICollection<FormatBook> Formats => _formats.ToList();
        public virtual ICollection<TagBook> Tags => _tags.ToList();

        public static Book Create(string bookTitle, string edition, Guid publisherID, string publishYear, string isbn, Guid languageID)
        {
            var book = new Book(Guid.NewGuid(), bookTitle, edition, publisherID, publishYear, isbn, languageID);
            book.IsActive = true;
            return book;
        }

        public Result<AuthorBook> AddAuthor(Guid authorID)
        {
            if (_authors.Any(ab => ab.AuthorID == authorID))
            {
                return Result.Failure<AuthorBook>(new Error("Author.AlreadyAdded", "Author is already added to the book", ErrorType.Failure));
            }
            var authorBook = AuthorBook.Create(authorID, Id);
            _authors.Add(authorBook.Value);
            return Result.Success(authorBook.Value);
        }

        public Result<AuthorBook> RemoveAuthor(Guid authorID)
        {
            var authorBook = _authors.SingleOrDefault(ab => ab.AuthorID == authorID);
            if (authorBook == null)
            {
                return Result.Failure<AuthorBook>(new Error("Author.NotFound", "Author is not added to the book", ErrorType.NotFound));
            }
            _authors.Remove(authorBook);
            return Result.Success(authorBook);
        }

        public Result<FormatBook> AddFormat(Guid formatID)
        {
            if (_formats.Any(fb => fb.FormatID == formatID))
            {
                return Result.Failure<FormatBook>(new Error("Format.AlreadyAdded", "Format is already added to the book", ErrorType.Failure));
            }
            var formatBook = FormatBook.Create(formatID, Id);
            _formats.Add(formatBook.Value);
            return Result.Success(formatBook.Value);
        }

        public Result<FormatBook> RemoveFormat(Guid formatID)
        {
            var formatBook = _formats.SingleOrDefault(fb => fb.FormatID == formatID);
            if (formatBook == null)
            {
                return Result.Failure<FormatBook>(new Error("Format.NotFound", "Format is not added to the book", ErrorType.NotFound));
            }
            _formats.Remove(formatBook);
            return Result.Success(formatBook);
        }

        public Result<TagBook> AddTag(Guid tagID)
        {
            if (_tags.Any(tb => tb.TagID == tagID))
            {
                return Result.Failure<TagBook>(new Error("Tag.AlreadyAdded", "Tag is already added to the book", ErrorType.Failure));
            }
            var tagBook = TagBook.Create(tagID, Id);
            _tags.Add(tagBook.Value);
            return Result.Success(tagBook.Value);
        }

        public Result<TagBook> RemoveTag(Guid tagID)
        {
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
