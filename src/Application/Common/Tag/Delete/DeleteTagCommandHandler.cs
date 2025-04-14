using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    internal sealed class DeleteTagCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteTagCommand>
    {
        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await context.Tags.FindAsync(request.TagId, cancellationToken);
            if (tag == null)
            {
                return Result.Failure(new Error("Tag.NotFound", $"Tag with {request.TagId} ID is not found.", ErrorType.NotFound));
            }
            tag.Inactivate();
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
