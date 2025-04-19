using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    public sealed class UpdatePublisherCommandHandler : ICommandHandler<UpdatePublisherCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(request.PublisherId);

            if (publisher == null)
            {
                return Result.Failure(new Error("Publisher.NotFound", "The publisher was not found.", ErrorType.NotFound));
            }

            var updateResult = publisher.Update(request.PublisherName);

            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await _unitOfWork.PublisherRepository.UpdateAsync(publisher);

            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                return Result.Failure(new Error("Publisher.UpdateFailed", "Failed to update the publisher.", ErrorType.Failure));
            }

            return Result.Success();
        }
    }
}
