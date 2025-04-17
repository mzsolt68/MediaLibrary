using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class UpdateLanguageCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateLanguageCommand>
    {
        public async Task<Result> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await context.LanguageRepository.GetByIdAsync(request.LanguageId);
            if (language == null)
            {
                return Result.Failure(new Error("Language.NotFound", $"Language with ID {request.LanguageId} was not found.", ErrorType.NotFound));
            }

            var updateResult = language.Update(request.LanguageName);
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }
            await context.LanguageRepository.UpdateAsync(language);
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return Result.Failure(new Error("Language.UpdateFailed", "Failed to update language.", ErrorType.Problem));
            }
            return Result.Success();
        }
    }
}
