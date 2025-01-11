using Application.Dto.Common;
using MediaLibrary.Entities.Models.Common;

namespace MediaLibrary.Common
{
    public static class ConvertCommonObjects
    {
        /// <summary>
        /// Converts a Genre DTO to DB object
        /// </summary>
        /// <param name="genre">A DTO to convert</param>
        /// <returns>DB object</returns>
        public static Genre AsGenre(this GenreDTO genre)
        {
            return new Genre()
            {
                GenreID = genre.GenreID,
                GenreName = genre.GenreName,
                GenreType = genre.GenreType
            };
        }

        /// <summary>
        /// Converts a Genre DB object to DTO
        /// </summary>
        /// <param name="genre">A DB object to convert</param>
        /// <returns>DTO object</returns>
        public static GenreDTO AsGenreDTO(this Genre genre)
        {
            return new GenreDTO()
            {
                GenreID = genre.GenreID,
                GenreName = genre.GenreName,
                GenreType = genre.GenreType
            };
        }

        /// <summary>
        /// Converts a Language DTO to DB object
        /// </summary>
        /// <param name="genre">A DTO to convert</param>
        /// <returns>DB object</returns>
        public static Language AsLanguage(this LanguageDTO language)
        {
            return new Language()
            {
                LanguageID = language.LanguageID,
                LanguageName = language.LanguageName
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
                LanguageID = language.LanguageID,
                LanguageName = language.LanguageName
            };
        }

        /// <summary>
        /// Converts a Tag DTO to DB object
        /// </summary>
        /// <param name="genre">A DTO to convert</param>
        /// <returns>DB object</returns>
        public static Tag AsTag(this TagDTO tag)
        {
            return new Tag()
            {
                TagID = tag.TagID,
                TagName = tag.TagName
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
                TagID = tag.TagID,
                TagName = tag.TagName
            };
        }
    }
}
