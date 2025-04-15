using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed record UpdateGenreCommand(Guid GenreId, string GenreName, string GenreType) : ICommand;
}
