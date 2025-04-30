using Application.Abstractions.Messaging;
using Application.Dto.Common;
using System.Linq.Expressions;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all available languages.
    /// </summary>
    public sealed record GetLanguagesQuery<T>(Expression<Func<T, bool>> Predicate) : IQuery<List<LanguageDTO>>;
}
