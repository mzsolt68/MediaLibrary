using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed record UpdateLanguageCommand(Guid LanguageId, string LanguageName) : ICommand;
}
