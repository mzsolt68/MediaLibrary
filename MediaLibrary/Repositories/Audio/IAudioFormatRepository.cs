using MediaEntities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Audio
{
    interface IAudioFormatRepository
    {
        void AddFormat(AudioFormat newFormat);
        void DeleteFormat(AudioFormat deletedFormat);
        void UpdateFormat(AudioFormat updatedFormat);
        AudioFormat GetFormatById(int? id);
        ICollection<AudioFormat> GetFormats();
    }
}
