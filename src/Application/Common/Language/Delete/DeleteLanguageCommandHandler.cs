using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    internal sealed class DeleteLanguageCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteLanguageCommand>
    {
        public async Task<Result> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await context.LanguageRepository.GetByIdAsync(request.LanguageId);
            if (language == null)
            {
                return Result.Failure(new Error("Language.NotFound", $"Language with {request.LanguageId} ID is not found.", ErrorType.NotFound));
            }
            language.Inactivate();
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return Result.Failure(new Error("Language.DeletionFailed", "Failed to delete language.", ErrorType.Problem));
            }
            return Result.Success();
        }
    }
}
