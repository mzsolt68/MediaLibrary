using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed record UpdateTagCommand(Guid TagId, string TagName) : ICommand;
}
