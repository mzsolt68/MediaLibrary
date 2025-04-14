using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed record DeleteTagCommand(Guid TagId) : ICommand
    {
    }
}
