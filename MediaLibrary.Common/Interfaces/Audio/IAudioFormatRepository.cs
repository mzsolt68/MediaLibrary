using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Audio
{
    public interface IAudioFormatRepository
    {
        Task<AudioFormat> AddFormat(AudioFormat newFormat);
        void DeleteFormat(AudioFormat deletedFormat);
        Task<AudioFormat> UpdateFormat(AudioFormat updatedFormat);
        Task<AudioFormat> GetFormatById(int? id);
        Task<ICollection<AudioFormat>> GetFormats();
    }
}
