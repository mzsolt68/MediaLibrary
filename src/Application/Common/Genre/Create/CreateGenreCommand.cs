using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to create a new genre with the specified name and type.
    /// </summary>
    /// <param name="GenreName">The name of the genre to create. This value cannot be null or empty.</param>
    /// <param name="GenreType">The type of the genre to create. This value cannot be null or empty.</param>
    public sealed record CreateGenreCommand(string GenreName, string GenreType) : ICommand<Guid>;
}
