using Application.Abstractions.Messaging;

namespace Application.Common
{
    public sealed class CreateTagCommand : ICommand<Guid>
    {
        public string TagName { get; set; }
    }
}
