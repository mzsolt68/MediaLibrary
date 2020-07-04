using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Dto.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Audio
{
    public interface ISongRepository
    {
        Task<Song> AddSong(Song newSong, ICollection<int> performers);
        Task<int> DeleteSong(int? id);
        Task<Song> UpdateSong(Song updatedSong, ICollection<int> performers);
        Task<Song> GetSongById(int? id);
        Task<ICollection<Song>> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        Task<ICollection<SongPerformer>> GetPerformersOfSong(int? songId);
        Task<int> GetSongCount();
    }
}
