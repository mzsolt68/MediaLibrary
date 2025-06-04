using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to create a new tag with the specified name.
    /// </summary>
    /// <param name="TagName">The name of the tag to be created. Must not be null or empty.</param>
    public sealed record CreateTagCommand(string TagName) : ICommand<Guid>;
}
