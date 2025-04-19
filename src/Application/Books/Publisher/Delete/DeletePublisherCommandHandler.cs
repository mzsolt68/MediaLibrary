using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books.Publisher.Delete
{
    public sealed class DeletePublisherCommandHandler : ICommandHandler<DeletePublisherCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(request.PublisherId);

            if (publisher == null)
            {
                return Result.Failure(new Error("Publisher.NotFound", "The publisher was not found.", ErrorType.NotFound));
            }

            publisher.SetActiveState(false);
            await _unitOfWork.PublisherRepository.DeleteBooks(request.PublisherId);

            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                return Result.Failure(new Error("Publisher.DeleteFailed", "Failed to delete the publisher.", ErrorType.Conflict));
            }

            return Result.Success();
        }
    }
}
