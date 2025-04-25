using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    /// <summary>
    /// Configures the database schema for the <see cref="Language"/> entity.
    /// </summary>
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        /// <summary>
        /// Configures the properties and relationships of the <see cref="Language"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="Language"/> entity.</param>
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            // Specifies the table name for the Language entity.
            builder.ToTable("Languages");

            // Configures the primary key for the Language entity.
            builder.HasKey(l => l.Id);

            // Configures the LanguageName property to be required and have a maximum length of 50.
            builder.Property(l => l.LanguageName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
