using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class CreateTagCommandHandler(IUnitOfWork context) : ICommandHandler<CreateTagCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tagResult = Tag.Create(request.TagName);
            if (tagResult.IsFailure)
            {
                return Result.Failure<Guid>(new Error(tagResult.Error.Code, tagResult.Error.Message, tagResult.Error.Type));
            }

            await context.TagRepository.AddAsync(tagResult.Value);
            int result = await context.SaveChangesAsync(cancellationToken);
            if(result == 0)
            {
                return Result.Failure<Guid>(new Error("Tag.CreateFailed", "Failed to create tag.", ErrorType.Problem));
            }
            return Result.Success(tagResult.Value.Id);
        }
    }
}
