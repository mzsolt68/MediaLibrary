using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed class CreateLanguageCommand : ICommand<Guid>
    {
        public string LanguageName { get; set; }
    }

}
