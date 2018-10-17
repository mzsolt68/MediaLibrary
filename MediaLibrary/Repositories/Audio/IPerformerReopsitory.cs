using MediaLibrary.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Audio
{
    interface IPerformerReopsitory
    {
        void AddPerformer(Performer newPerformer);
        void DeletePerformer(Performer deletedPerformer);
        void UpdatePerformer(Performer updatedPerformer);
        Performer GetPerformerById(int id);
        List<Performer> GetPerformers();
        List<Song> SongsOfPerformer(Performer performer);
    }
}
