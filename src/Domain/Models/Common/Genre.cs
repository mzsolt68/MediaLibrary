using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
{
    /// <summary>
    /// Represents a genre entity with a name and type.
    /// </summary>
    public class Genre : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class with the specified identifier.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private Genre(Guid id) : base(id) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier for the genre.</param>
        /// <param name="genreName">The name of the genre.</param>
        /// <param name="genreType">The type of the genre.</param>
        private Genre(Guid guid, string genreName, string genreType) : base(guid)
        {
            GenreName = genreName;
            GenreType = genreType;
        }

        /// <summary>
        /// Gets the name of the genre.
        /// </summary>
        [Required]
        [Display(Name = "Műfaj")]
        public string GenreName { get; private set; }

        /// <summary>
        /// Gets the type of the genre.
        /// </summary>
        [Required]
        public string GenreType { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Genre"/> instance.
        /// </summary>
        /// <param name="genreName">The name of the genre.</param>
        /// <param name="genreType">The type of the genre.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="Genre"/> instance if successful,
        /// or an error if validation fails.
        /// </returns>
        public static Result<Genre> Create(string genreName, string genreType)
        {
            if (string.IsNullOrWhiteSpace(genreName))
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

        /// <summary>
        /// Updates the properties of the genre.
        /// </summary>
        /// <param name="genreName">The new name of the genre.</param>
        /// <param name="genreType">The new type of the genre.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the update operation.
        /// </returns>
        public Result Update(string genreName, string genreType)
        {
            if (string.IsNullOrWhiteSpace(genreName))
            {
                return Result.Failure(new Error("GenreName.Missing", "Genre name is missing", ErrorType.Validation));
            }
            if (string.IsNullOrWhiteSpace(genreType))
            {
                return Result.Failure(new Error("GenreType.Missing", "Genre type is missing", ErrorType.Validation));
            }
            GenreName = genreName;
            GenreType = genreType;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
