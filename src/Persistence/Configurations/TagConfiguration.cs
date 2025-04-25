using Domain.Models.Books;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    /// <summary>
    /// Configures the database schema for the <see cref="Tag"/> entity.
    /// </summary>
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        /// <summary>
        /// Configures the properties and relationships of the <see cref="Tag"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="Tag"/> entity.</param>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            // Configure the table name for the Tag entity.
            builder.ToTable("Tags");

            // Configure the primary key for the Tag entity.
            builder.HasKey(t => t.Id);

            // Configure the TagName property to be required and have a maximum length of 50.
            builder.Property(t => t.TagName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
