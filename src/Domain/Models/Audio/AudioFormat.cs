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

        public static AudioFormat Create(string audioFormatName)
        {
            var audioFormat = new AudioFormat(Guid.NewGuid(), audioFormatName);
            audioFormat.IsActive = true;
            return audioFormat;
        }

        public void UpdateName(string audioFormatName)
        {
            AudioFormatName = audioFormatName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetActiveState(bool newState)
        {
            IsActive = newState;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
