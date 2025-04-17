using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class CreateLanguageCommandHandler(IUnitOfWork context) : ICommandHandler<CreateLanguageCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var languageResult = Language.Create(request.LanguageName);
            if (languageResult.IsFailure)
            {
                return Result.Failure<Guid>(new Error(languageResult.Error.Code, languageResult.Error.Message, languageResult.Error.Type));
            }

            await context.LanguageRepository.AddAsync(languageResult.Value);
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return Result.Failure<Guid>(new Error("Language.CreationFailed", "Failed to create language.", ErrorType.Problem));
            }
            return Result.Success(languageResult.Value.Id);
        }
    }
}
