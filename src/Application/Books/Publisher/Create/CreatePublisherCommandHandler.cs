using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    public sealed class CreatePublisherCommandHandler : ICommandHandler<CreatePublisherCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            // Create the Publisher entity
            var publisherResult = Domain.Models.Books.Publisher.Create(request.PublisherName);

            if (publisherResult.IsFailure)
            {
                return Result.Failure<Guid>(publisherResult.Error);
            }

            // Add the Publisher to the repository
            await _unitOfWork.PublisherRepository.AddAsync(publisherResult.Value);

            // Save changes
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                return Result.Failure<Guid>(new Error("Publisher.CreateFailed", "Failed to create publisher.", ErrorType.Failure));
            }

            return Result.Success(publisherResult.Value.Id);
        }
    }
}
