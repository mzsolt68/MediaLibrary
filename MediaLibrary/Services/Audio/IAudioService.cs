using MediaLibrary.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Services.Audio
{
    public interface IAudioService
    {
        void AddAlbum(Album newAlbum);
        void DeleteAlbum(Album deletedAlbum);
        void UpdateAlbum(Album updatedAlbum);
        Album GetAlbumById(int id);
        List<Album> GetAlbums();
        List<Song> GetSongsOfAlbum(Album album);

        void AddFormat(AudioFormat newFormat);
        void DeleteFormat(AudioFormat deletedFormat);
        void UpdateFormat(AudioFormat updatedFormat);
        AudioFormat GetFormatById(int id);
        List<AudioFormat> GetFormats();

        void AddPerformer(Performer newPerformer);
        void DeletePerformer(Performer deletedPerformer);
        void UpdatePerformer(Performer updatedPerformer);
        Performer GetPerformerById(int id);
        List<Performer> GetPerformers();
        List<Song> SongsOfPerformer(Performer performer);

        void AddSong(Song newSong);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong);
        Song GetSongById(int id);
        List<Song> GetSongs();
        List<Album> GetAlbumsOfSong(Song song);
        List<Performer> GetPerformersOfSong(Song song);
    }
}
