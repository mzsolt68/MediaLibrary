using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Audio
{
    public interface IPerformerReopsitory
    {
        void AddPerformer(SongPerformer newPerformer);
        void DeletePerformer(SongPerformer deletedPerformer);
        void UpdatePerformer(SongPerformer updatedPerformer);
        Task<SongPerformer> GetPerformerById(int? id);
        Task<ICollection<SongPerformer>> GetPerformers();
        ICollection<PerformerSong> SongsOfPerformer(SongPerformer performer);
        Task<int> GetPerformerCount();
    }
}
