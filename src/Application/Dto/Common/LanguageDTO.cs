namespace Application.Dto.Common
{
    /// <summary>
    /// Represents a data transfer object for a language.
    /// </summary>
    public class LanguageDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the language.
        /// </summary>
        public Guid LanguageID { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        public string LanguageName { get; set; }
    }
}
