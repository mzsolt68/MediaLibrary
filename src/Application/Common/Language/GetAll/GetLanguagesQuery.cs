using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve a list of languages based on the specified search parameters.
    /// </summary>
    /// <param name="SearchParams">The search parameters used to filter the list of languages. This parameter cannot be null.</param>
    public sealed record GetLanguagesQuery(SearchParamsDTO SearchParams) : IQuery<List<LanguageDTO>>;
}
