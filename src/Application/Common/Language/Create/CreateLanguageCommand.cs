using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to create a new language with the specified name.
    /// </summary>
    /// <param name="LanguageName">The name of the language to be created. Cannot be null or empty.</param>
    public sealed record CreateLanguageCommand(string LanguageName) : ICommand<Guid>;
}
