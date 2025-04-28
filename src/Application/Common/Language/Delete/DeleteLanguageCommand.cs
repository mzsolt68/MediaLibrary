using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to delete a language.
    /// </summary>
    /// <param name="LanguageId">The unique identifier of the language to be deleted.</param>
    public sealed record DeleteLanguageCommand(Guid LanguageId) : ICommand
    {
    }
}
