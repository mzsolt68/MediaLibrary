using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class UpdateTagCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateTagCommand>
    {
        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await context.TagRepository.GetByIdAsync(request.TagId);
            if (tag == null)
            {
                return Result.Failure(new Error("Tag.NotFound", $"Tag with ID {request.TagId} was not found.", ErrorType.NotFound));
            }

            var updateResult = tag.Update(request.TagName);
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return Result.Failure(new Error("Tag.UpdateFailed", "Failed to update tag.", ErrorType.Problem));
            }
            return Result.Success();
        }
    }
}
