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
        ICollection<Song> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<Performer> GetPerformersOfSong(Song song);
    }
}
