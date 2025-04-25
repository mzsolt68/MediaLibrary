using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    /// <summary>
    /// Represents an audio format entity in the domain.
    /// </summary>
    public class AudioFormat : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFormat"/> class with the specified identifier and name.
        /// </summary>
        /// <param name="id">The unique identifier for the audio format.</param>
        /// <param name="audioFormatName">The name of the audio format.</param>
        private AudioFormat(Guid id, string audioFormatName) : base(id)
        {
            AudioFormatName = audioFormatName;
        }

        /// <summary>
        /// Gets or sets the name of the audio format.
        /// </summary>
        [Required]
        [Display(Name = "Formátum neve")]
        public string AudioFormatName { get; set; }

        /// <summary>
        /// Creates a new <see cref="AudioFormat"/> instance.
        /// </summary>
        /// <param name="audioFormatName">The name of the audio format.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="AudioFormat"/> instance if successful,
        /// or an error if the audio format name is invalid.
        /// </returns>
        public static Result<AudioFormat> Create(string audioFormatName)
        {
            if (string.IsNullOrWhiteSpace(audioFormatName))
            {
                return Result.Failure<AudioFormat>(new Error("AudioFormatName.Missing", "Audio format name is missing", ErrorType.Validation));
            }
            var audioFormat = new AudioFormat(Guid.NewGuid(), audioFormatName);
            audioFormat.IsActive = true;
            return Result.Success(audioFormat);
        }

        /// <summary>
        /// Updates the name of the audio format.
        /// </summary>
        /// <param name="audioFormatName">The new name of the audio format.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success if the name was updated,
        /// or an error if the new name is invalid.
        /// </returns>
        public Result UpdateName(string audioFormatName)
        {
            if (string.IsNullOrWhiteSpace(audioFormatName))
            {
                return Result.Failure(new Error("AudioFormatName.Missing", "Audio format name is missing", ErrorType.Validation));
            }
            AudioFormatName = audioFormatName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
