using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve a genre by its unique identifier.
    /// </summary>
    public sealed class GetGenreByIdQuery : IQuery<GenreDTO>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the genre to retrieve.
        /// </summary>
        public Guid GenreId { get; set; }
    }
}
