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
        Task<int> DeleteFormat(int? id);
        Task<AudioFormat> UpdateFormat(AudioFormat updatedFormat);
        Task<AudioFormat> GetFormatById(int? id);
        Task<ICollection<AudioFormat>> GetFormats();
    }
}
