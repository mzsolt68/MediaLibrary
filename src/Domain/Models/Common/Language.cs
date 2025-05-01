using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
{
    /// <summary>
    /// Represents a language entity in the domain.
    /// </summary>
    public class Language : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class with the specified identifier.
        /// It is used EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private Language(Guid id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class with the specified identifier and language name.
        /// </summary>
        /// <param name="guid">The unique identifier for the language.</param>
        /// <param name="languageName">The name of the language.</param>
        private Language(Guid guid, string languageName) : base(guid)
        {
            LanguageName = languageName;
        }

        /// <summary>
        /// Gets the name of the language.
        /// </summary>
        [Required]
        [Display(Name = "Nyelv")]
        public string LanguageName { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Language"/> instance.
        /// </summary>
        /// <param name="languageName">The name of the language to create.</param>
        /// <returns>
        /// A <see cref="Result{Language}"/> indicating success or failure. 
        /// If successful, contains the created <see cref="Language"/> instance.
        /// </returns>
        public static Result<Language> Create(string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
            {
                return Result.Failure<Language>(new Error("LanguageName.Missing", "Language name is missing", ErrorType.Validation));
            }
            var language = new Language(Guid.NewGuid(), languageName);
            language.IsActive = true;
            return language;
        }

        /// <summary>
        /// Updates the name of the language.
        /// </summary>
        /// <param name="languageName">The new name of the language.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure.
        /// </returns>
        public Result Update(string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
            {
                return Result.Failure(new Error("LanguageName.Missing", "Language name is missing", ErrorType.Validation));
            }
            LanguageName = languageName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
