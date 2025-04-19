using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    public sealed class UpdateBookFormatCommandHandler : ICommandHandler<UpdateBookFormatCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookFormatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateBookFormatCommand request, CancellationToken cancellationToken)
        {
            var bookFormat = await _unitOfWork.BookFormatRepository.GetByIdAsync(request.BookFormatId);

            if (bookFormat == null)
            {
                return Result.Failure(new Error("BookFormat.NotFound", "The book format was not found.", ErrorType.NotFound));
            }

            var updateResult = bookFormat.Update(request.BookFormatName);

            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await _unitOfWork.BookFormatRepository.UpdateAsync(bookFormat);

            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                return Result.Failure(new Error("BookFormat.UpdateFailed", "Failed to update book format.", ErrorType.Failure));
            }

            return Result.Success();
        }
    }
}
