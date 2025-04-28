using Domain.Models.Common;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a repository interface for managing <see cref="Language"/> entities.
    /// </summary>
    public interface ILanguageRepository : IGenericRepository<Language>
    {
    }
}
