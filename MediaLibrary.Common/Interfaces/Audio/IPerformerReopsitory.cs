using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Audio
{
    public interface IPerformerReopsitory
    {
        Task<SongPerformer> AddPerformer(SongPerformer newPerformer);
        Task<int> DeletePerformer(int? id);
        Task<SongPerformer> UpdatePerformer(SongPerformer updatedPerformer);
        Task<SongPerformer> GetPerformerById(int? id);
        Task<ICollection<SongPerformer>> GetPerformers();
        Task<int> GetPerformerCount();
    }
}
