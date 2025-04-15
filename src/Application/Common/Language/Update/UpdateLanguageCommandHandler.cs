using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class UpdateLanguageCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateLanguageCommand>
    {
        public async Task<Result> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await context.Languages.FindAsync(request.LanguageId, cancellationToken);
            if (language == null)
            {
                return Result.Failure(new Error("Language.NotFound", $"Language with ID {request.LanguageId} was not found.", ErrorType.NotFound));
            }

            var updateResult = language.Update(request.LanguageName);
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
