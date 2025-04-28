using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to update a language.
    /// </summary>
    /// <param name="LanguageId">The unique identifier of the language to update.</param>
    /// <param name="LanguageName">The new name of the language.</param>
    public sealed record UpdateLanguageCommand(Guid LanguageId, string LanguageName) : ICommand;
}
