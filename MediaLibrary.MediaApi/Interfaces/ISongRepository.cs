using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Entities.Dto.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Interfaces
{
    interface ISongRepository
    {
        void AddSong(Song newSong, List<SongPerformerDto> performers);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong, List<SongPerformerDto> performers);
        Song GetSongById(int? id);
        ICollection<Song> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<Performer> GetPerformersOfSong(Song song);
        int GetSongCount();
    }
}
