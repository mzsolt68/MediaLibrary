using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed record DeleteGenreCommand(Guid GenreId) : ICommand
    {
    }
}
