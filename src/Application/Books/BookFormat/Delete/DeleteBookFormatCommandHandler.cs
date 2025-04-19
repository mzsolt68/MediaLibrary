using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    public sealed class DeleteBookFormatCommandHandler : ICommandHandler<DeleteBookFormatCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookFormatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteBookFormatCommand request, CancellationToken cancellationToken)
        {
            var bookFormat = await _unitOfWork.BookFormatRepository.GetByIdAsync(request.BookFormatId);

            if (bookFormat == null)
            {
                return Result.Failure(new Error("BookFormat.NotFound", "The book format was not found.", ErrorType.NotFound));
            }

            bookFormat.SetActiveState(false);

            //TODO: Check if the book format is used in any book

            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                return Result.Failure(new Error("BookFormat.DeleteFailed", "Failed to delete book format.", ErrorType.Conflict));
            }

            return Result.Success();
        }
    }
}
