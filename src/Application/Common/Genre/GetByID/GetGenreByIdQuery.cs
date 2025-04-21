using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    public sealed class GetGenreByIdQuery() : IQuery<GenreDTO>
    {
        public Guid GenreId { get; set; }
    }
}
