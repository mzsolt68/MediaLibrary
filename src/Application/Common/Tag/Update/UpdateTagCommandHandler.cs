using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class UpdateTagCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateTagCommand>
    {
        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await context.Tags.FindAsync(request.TagId, cancellationToken);
            if (tag == null)
            {
                return Result.Failure(new Error("Tag.NotFound", $"Tag with ID {request.TagId} was not found.", ErrorType.NotFound));
            }

            var updateResult = tag.Update(request.TagName);
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
