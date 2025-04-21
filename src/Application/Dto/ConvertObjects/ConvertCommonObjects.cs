using Application.Dto.Common;
using Domain.Models.Common;

namespace Application.Dto.ConvertObjects
{
    public static class ConvertCommonObjects
    {
        /// <summary>
        /// Converts a Genre DB object to DTO
        /// </summary>
        /// <param name="genre">A DB object to convert</param>
        /// <returns>DTO object</returns>
        public static GenreDTO AsGenreDTO(this Genre genre)
        {
            return new GenreDTO()
            {
                GenreID = genre.Id,
                GenreName = genre.GenreName,
                GenreType = genre.GenreType
            };
        }

        /// <summary>
        /// Converts a Language DB object to DTO
        /// </summary>
        /// <param name="genre">A DB object to convert</param>
        /// <returns>DTO object</returns>
        public static LanguageDTO AsLanguageDTO(this Language language)
        {
            return new LanguageDTO()
            {
                LanguageID = language.Id,
                LanguageName = language.LanguageName
            };
        }

        /// <summary>
        /// Converts a Tag DB object to DTO
        /// </summary>
        /// <param name="genre">A DB object to convert</param>
        /// <returns>DTO object</returns>
        public static TagDTO AsTagDTO(this Tag tag)
        {
            return new TagDTO()
            {
                TagID = tag.Id,
                TagName = tag.TagName
            };
        }
    }
}
