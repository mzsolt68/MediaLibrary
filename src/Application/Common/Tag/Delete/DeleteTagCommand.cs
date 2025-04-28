using Application.Abstractions.Messaging;

namespace Application.Common
{
    /// <summary>
    /// Represents a command to delete a tag.
    /// </summary>
    /// <param name="TagId">The unique identifier of the tag to be deleted.</param>
    public sealed record DeleteTagCommand(Guid TagId) : ICommand
    {
    }
}
