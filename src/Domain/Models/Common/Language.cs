using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
{
    public class Language : Entity
    {
        private Language(Guid guid, string languageName) : base(guid)
        {
            LanguageName = languageName;
        }

        [Required]
        [Display(Name = "Nyelv")]
        public string LanguageName { get; private set; }

        public static Language Create(string languageName)
        {
            var language = new Language(Guid.NewGuid(), languageName);
            language.IsActive = true;
            return language;
        }

        public void Update(string languageName)
        {
            LanguageName = languageName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
