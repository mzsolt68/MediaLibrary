using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all available languages.
    /// </summary>
    public sealed record GetLanguagesQuery<T>(SearchParamsDTO SearchParams) : IQuery<List<LanguageDTO>>;
}
