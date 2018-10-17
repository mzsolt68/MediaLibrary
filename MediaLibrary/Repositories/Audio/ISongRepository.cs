using MediaLibrary.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Audio
{
    interface ISongRepository
    {
        void AddSong(Song newSong);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong);
        Song GetSongById(int? id);
        List<Song> GetSongs();
        List<Album> GetAlbumsOfSong(Song song);
        List<Performer> GetPerformersOfSong(Song song);
    }
}
