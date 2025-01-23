
using Application.Abstractions.Messaging;
using System.Windows.Input;

namespace Application.Common.Genre.Create
{
    public sealed class CreateGenreCommand : ICommand<Guid>
    {
        public string GenreName { get; set; }
        public string GenreType { get; set; }
    }
}
