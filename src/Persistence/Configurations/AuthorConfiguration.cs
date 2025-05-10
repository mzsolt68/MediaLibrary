using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AuthorLastName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.AuthorFirstName)
                .HasMaxLength(50);
            builder.Property(a => a.AuthorMiddleName)
                .HasMaxLength(50);
            
        }
    }
}
