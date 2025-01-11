using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Entities.Data
{
    public static class FillBookTestData
    {
        public static void Fill(ModelBuilder builder)
        {
            FillAuthors(builder);
            FillBookFormats(builder);
            FillPublishers(builder);
            FillBooks(builder);
        }

        private static void FillAuthors(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(
                new Author { AuthorID = 1, AuthorFirstName = "György", AuthorLastName = "Tilesch" },
                new Author { AuthorID = 2, AuthorFirstName = "Omar", AuthorLastName = "Hatamleh" },
                new Author { AuthorID = 3, AuthorFirstName = "Andrew", AuthorMiddleName = "S.", AuthorLastName = "Tannenbaum" },
                new Author { AuthorID = 4, AuthorFirstName = "Cay", AuthorLastName = "Horstmann" }
            );
        }

        private static void FillBookFormats(ModelBuilder builder)
        {
            builder.Entity<BookFormat>().HasData(
                new BookFormat { BookFormatID = 1, BookFormatName = "papír" },
                new BookFormat { BookFormatID = 2, BookFormatName = "epub" },
                new BookFormat { BookFormatID = 3, BookFormatName = "mobi" },
                new BookFormat { BookFormatID = 4, BookFormatName = "azw" },
                new BookFormat { BookFormatID = 5, BookFormatName = "pdf" },
                new BookFormat { BookFormatID = 6, BookFormatName = "djvu" }
            );
        }

        private static void FillPublishers(ModelBuilder builder)
        {
            builder.Entity<Publisher>().HasData(
                new Publisher { PublisherID = 1, PublisherName = "Libri" },
                new Publisher { PublisherID = 2, PublisherName = "Panem" },
                new Publisher { PublisherID = 3, PublisherName = "John Wiley & Sons, Inc." }
            );
        }

        private static void FillBooks(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book { BookID = 1, BookTitle = "Mesterséges intelligencia", Edition = "1", ISBN = "978-963-433-829-1", PublisherID = 1, PublishYear = "2021", LanguageID = 4 },
                new Book { BookID = 2, BookTitle = "Számítógép-hálózatok", Edition = "3", ISBN = "963-545-213-6", PublisherID = 2, PublishYear = "1999", LanguageID = 4 },
                new Book { BookID = 3, BookTitle = "Object-Oriented Design & Patterns", Edition = "2", ISBN = "0-471-74487-5", PublisherID = 3, PublishYear = "2006", LanguageID = 1 }
            );
            builder.Entity<AuthorBook>().HasData(
                new AuthorBook { AuthorID = 1, BookID = 1 },
                new AuthorBook { AuthorID = 2, BookID = 1 },
                new AuthorBook { AuthorID = 3, BookID = 2 },
                new AuthorBook { AuthorID = 4, BookID = 3 }
            );
            builder.Entity<FormatBook>().HasData(
                new FormatBook { BookID = 1, FormatID = 1 },
                new FormatBook { BookID = 2, FormatID = 2 },
                new FormatBook { BookID = 3, FormatID = 5 }
            );
        }
    }
}
