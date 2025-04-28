using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to create a new language.
    /// </summary>
    public sealed class CreateLanguageCommand : ICommand<Guid>
    {
        /// <summary>
        /// Gets or sets the name of the language to be created.
        /// </summary>
        public string LanguageName { get; set; }
    }

}
