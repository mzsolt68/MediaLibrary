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

        public static Result<Genre> Create(string genreName, string genreType)
        {
            if(string.IsNullOrWhiteSpace(genreName))
            {
                return Result.Failure<Genre>(new Error("GenreName.Missing", "Genre name is missing", ErrorType.Validation));
            }
            if (string.IsNullOrWhiteSpace(genreType))
            {
                return Result.Failure<Genre>(new Error("GenreType.Missing", "Genre type is missing", ErrorType.Validation));
            }
            var genre = new Genre(Guid.NewGuid(), genreName, genreType);
            genre.IsActive = true;
            return Result.Success(genre);
        }

        public void Update(string genreName, string genreType)
        {
            GenreName = genreName;
            GenreType = genreType;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
