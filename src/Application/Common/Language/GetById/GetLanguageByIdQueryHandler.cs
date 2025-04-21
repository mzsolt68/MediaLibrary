using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    public sealed class GetLanguageByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetLanguageByIdQuery, LanguageDTO>
    {
        public async Task<Result<LanguageDTO>> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            var language = await context.LanguageRepository.GetByIdAsync(request.LanguageId);

            if (language == null)
            {
                return Result.Failure<LanguageDTO>(new Error("Language.NotFound", "Language not found", ErrorType.NotFound));
            }

            return Result.Success(language.AsLanguageDTO());
        }
    }
}
