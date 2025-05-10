using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class BookFormatConfiguration : IEntityTypeConfiguration<BookFormat>
    {
        public void Configure(EntityTypeBuilder<BookFormat> builder)
        {
            builder.ToTable("BookFormats");
            builder.HasKey(bf => bf.Id);
            builder.Property(bf => bf.BookFormatName)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
