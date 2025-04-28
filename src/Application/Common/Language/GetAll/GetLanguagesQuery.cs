using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all available languages.
    /// </summary>
    public sealed record GetLanguagesQuery : IQuery<List<LanguageDTO>>;
}
