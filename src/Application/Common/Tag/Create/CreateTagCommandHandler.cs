using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class CreateTagCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateTagCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tagResult = Tag.Create(request.TagName);
            if (tagResult.IsFailure)
            {
                return Result.Failure<Guid>(new Error(tagResult.Error.Code, tagResult.Error.Message, tagResult.Error.Type));
            }

            context.Tags.Add(tagResult.Value);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(tagResult.Value.Id);
        }
    }
}
