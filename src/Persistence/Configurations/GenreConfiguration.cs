using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    /// <summary>
    /// Configures the database schema for the <see cref="Genre"/> entity.
    /// </summary>
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        /// <summary>
        /// Configures the properties and relationships of the <see cref="Genre"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="Genre"/> entity.</param>
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            // Configures the table name for the Genre entity.
            builder.ToTable("Genres");

            // Configures the primary key for the Genre entity.
            builder.HasKey(g => g.Id);

            // Configures the GenreName property to be required and have a maximum length of 100.
            builder.Property(g => g.GenreName)
                .IsRequired()
                .HasMaxLength(100);

            // Configures the GenreType property to be required and have a maximum length of 50.
            builder.Property(g => g.GenreType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
