using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookTitle)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(b => b.ISBN)
                .HasMaxLength(13);
            builder.Property(b => b.PublishYear)
                .HasMaxLength(4);
            builder.Property(b => b.Edition)
                .HasMaxLength(5);
            builder.HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<AuthorBook>(j => j.ToTable("BookAuthors"));
            builder.HasMany(b => b.Formats)
                .WithMany(f => f.BooksInFormat)
                .UsingEntity<FormatBook>(j => j.ToTable("FormatsOfBooks"));
            builder.HasMany(b => b.Tags)
                .WithMany(t => t.BooksOfTag)
                .UsingEntity<TagBook>(j => j.ToTable("TagsOfBooks"));
        }
    }
}
