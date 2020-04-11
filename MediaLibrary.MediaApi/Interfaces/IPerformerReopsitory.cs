using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Interfaces
{
    interface IPerformerReopsitory
    {
        void AddPerformer(Performer newPerformer);
        void DeletePerformer(Performer deletedPerformer);
        void UpdatePerformer(Performer updatedPerformer);
        Task<Performer> GetPerformerById(int? id);
        Task<ICollection<Performer>> GetPerformers();
        ICollection<PerformerSong> SongsOfPerformer(Performer performer);
        int GetPerformerCount();
    }
}
