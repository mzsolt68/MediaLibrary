using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to delete a genre.
    /// </summary>
    /// <param name="GenreId">The unique identifier of the genre to be deleted.</param>
    public sealed record DeleteGenreCommand(Guid GenreId) : ICommand;
}
