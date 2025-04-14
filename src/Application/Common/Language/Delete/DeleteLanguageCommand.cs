using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed record DeleteLanguageCommand(Guid LanguageId) : ICommand
    {
    }
}
