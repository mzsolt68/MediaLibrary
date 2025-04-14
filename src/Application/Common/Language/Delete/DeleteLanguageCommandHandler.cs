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
    internal sealed class DeleteLanguageCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteLanguageCommand>
    {
        public async Task<Result> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await context.Languages.FindAsync(request.LanguageId, cancellationToken);
            if (language == null)
            {
                return Result.Failure(new Error("Language.NotFound", $"Language with {request.LanguageId} ID is not found.", ErrorType.NotFound));
            }
            language.Inactivate();
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
