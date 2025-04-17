using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    internal sealed class DeleteTagCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteTagCommand>
    {
        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await context.TagRepository.GetByIdAsync(request.TagId);
            if (tag == null)
            {
                return Result.Failure(new Error("Tag.NotFound", $"Tag with {request.TagId} ID is not found.", ErrorType.NotFound));
            }
            tag.Inactivate();
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return Result.Failure(new Error("Tag.DeleteFailed", "Failed to delete tag.", ErrorType.Problem));
            }
            return Result.Success();
        }
    }
}
