using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    public sealed class GetLanguagesQueryHandler(IUnitOfWork context) : IQueryHandler<GetLanguagesQuery, List<LanguageDTO>>
    {
        public async Task<Result<List<LanguageDTO>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = await context.LanguageRepository.GetAllAsync();

            if (languages == null || !languages.Any())
            {
                return Result.Failure<List<LanguageDTO>>(new Error("Languages.NotFound", "No languages found", ErrorType.NotFound));
            }

            var languageDtos = languages.Select(language => language.AsLanguageDTO()).ToList();
            return Result.Success(languageDtos);
        }
    }
}
