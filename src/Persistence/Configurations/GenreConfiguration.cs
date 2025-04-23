using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.GenreName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(g => g.GenreType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
