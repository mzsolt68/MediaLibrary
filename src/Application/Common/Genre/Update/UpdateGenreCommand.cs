using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to update a genre.
    /// </summary>
    /// <param name="GenreId">The unique identifier of the genre to update.</param>
    /// <param name="GenreName">The new name of the genre.</param>
    /// <param name="GenreType">The new type of the genre.</param>
    public sealed record UpdateGenreCommand(Guid GenreId, string GenreName, string GenreType) : ICommand;
}
