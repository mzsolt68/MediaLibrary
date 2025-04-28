using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve a language by its unique identifier.
    /// </summary>
    /// <param name="LanguageId">The unique identifier of the language to retrieve.</param>
    /// <returns>A <see cref="LanguageDTO"/> containing the details of the language.</returns>
    public sealed record GetLanguageByIdQuery(Guid LanguageId) : IQuery<LanguageDTO>;
}
