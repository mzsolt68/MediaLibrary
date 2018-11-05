using MediaLibrary.Models.Audio;
using MediaLibrary.ViewModels.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Audio
{
    interface ISongRepository
    {
        void AddSong(Song newSong, List<SongPerformerViewModel> performers);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong, List<SongPerformerViewModel> performers);
        Song GetSongById(int? id);
        ICollection<Song> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<Performer> GetPerformersOfSong(Song song);
        int GetSongCount();
    }
}
