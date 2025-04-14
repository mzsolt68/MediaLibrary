using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class CreateLanguageCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateLanguageCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var languageResult = Language.Create(request.LanguageName);
            if (languageResult.IsFailure)
            {
                return Result.Failure<Guid>(new Error(languageResult.Error.Code, languageResult.Error.Message, languageResult.Error.Type));
            }

            context.Languages.Add(languageResult.Value);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(languageResult.Value.Id);
        }
    }
}
