using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    public sealed class GetGenresQuery : IQuery<List<GenreDTO>>;
}
