using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to update a tag.
    /// </summary>
    /// <param name="TagId">The unique identifier of the tag to be updated.</param>
    /// <param name="TagName">The new name of the tag.</param>
    public sealed record UpdateTagCommand(Guid TagId, string TagName) : ICommand;
}
