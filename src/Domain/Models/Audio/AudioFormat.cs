using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    public class AudioFormat : Entity
    {
        private AudioFormat(Guid id, string audioFormatName) : base(id)
        {
            AudioFormatName = audioFormatName;
        }
        [Required]
        [Display(Name = "Formátum neve")]
        public string AudioFormatName { get; set; }

        public static Result<AudioFormat> Create(string audioFormatName)
        {
            if(string.IsNullOrWhiteSpace(audioFormatName))
            {
                return Result.Failure<AudioFormat>(new Error("AudioFormatName.Missing", "Audio format name is missing", ErrorType.Validation));
            }
            var audioFormat = new AudioFormat(Guid.NewGuid(), audioFormatName);
            audioFormat.IsActive = true;
            return Result.Success(audioFormat);
        }

        public Result UpdateName(string audioFormatName)
        {
            if(string.IsNullOrWhiteSpace(audioFormatName))
            {
                return Result.Failure(new Error("AudioFormatName.Missing", "Audio format name is missing", ErrorType.Validation));
            }
            AudioFormatName = audioFormatName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public Result SetActiveState(bool newState)
        {
            IsActive = newState;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
