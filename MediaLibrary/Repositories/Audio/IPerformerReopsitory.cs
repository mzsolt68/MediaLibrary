using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Audio
{
    interface IPerformerReopsitory
    {
        void AddPerformer(SongPerformer newPerformer);
        void DeletePerformer(SongPerformer deletedPerformer);
        void UpdatePerformer(SongPerformer updatedPerformer);
        SongPerformer GetPerformerById(int? id);
        ICollection<SongPerformer> GetPerformers();
        ICollection<PerformerSong> SongsOfPerformer(SongPerformer performer);
        int GetPerformerCount();
    }
}
