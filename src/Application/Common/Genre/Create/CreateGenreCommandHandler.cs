
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common.Genre.Create
{
    internal sealed class CreateGenreCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateGenreCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
