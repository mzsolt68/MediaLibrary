using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to create a new tag.
    /// </summary>
    public sealed class CreateTagCommand : ICommand<Guid>
    {
        /// <summary>
        /// Gets or sets the name of the tag to be created.
        /// </summary>
        public string TagName { get; set; }
    }
}
