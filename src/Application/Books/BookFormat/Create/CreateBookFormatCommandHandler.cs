using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    public sealed class CreateBookFormatCommandHandler : ICommandHandler<CreateBookFormatCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookFormatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateBookFormatCommand request, CancellationToken cancellationToken)
        {
            var bookFormatResult = BookFormat.Create(request.BookFormatName);

            if (bookFormatResult.IsFailure)
            {
                return Result.Failure<Guid>(bookFormatResult.Error);
            }

            await _unitOfWork.BookFormatRepository.AddAsync(bookFormatResult.Value);

            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                return Result.Failure<Guid>(new Error("BookFormat.CreateFailed", "Failed to create book format.", ErrorType.Failure));
            }

            return Result.Success(bookFormatResult.Value.Id);
        }
    }
}
