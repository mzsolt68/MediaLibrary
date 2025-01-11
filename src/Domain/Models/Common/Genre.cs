using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
{
    public class Genre : Entity
    {
        private Genre(Guid guid, string genreName, string genreType) : base(guid)
        {
            GenreName = genreName;
            GenreType = genreType;
        }

        [Required]
        [Display(Name = "Műfaj")]
        public string GenreName { get; private set; }
        [Required]
        public string GenreType { get; private set; }

        public static Genre Create(string genreName, string genreType)
        {
            var genre = new Genre(Guid.NewGuid(), genreName, genreType);
            genre.IsActive = true;
            return genre;
        }

        public void Update(string genreName, string genreType)
        {
            GenreName = genreName;
            GenreType = genreType;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
