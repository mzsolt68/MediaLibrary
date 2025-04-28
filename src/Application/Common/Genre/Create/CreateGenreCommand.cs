using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to create a new genre.
    /// </summary>
    public sealed class CreateGenreCommand : ICommand<Guid>
    {
        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Gets or sets the type of the genre.
        /// </summary>
        public string GenreType { get; set; }
    }
}
