using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.ViewModels.Audio;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        Album GetAlbumById(int? id);
        ICollection<Album> GetAlbums();
        ICollection<AlbumSong> GetSongsOfAlbum(Album album);

        void AddFormat(AudioFormat newFormat);
        void DeleteFormat(AudioFormat deletedFormat);
        void UpdateFormat(AudioFormat updatedFormat);
        AudioFormat GetFormatById(int? id);
        ICollection<AudioFormat> GetFormats();
        IEnumerable<SelectListItem> GetFormatsToViews();

        void AddPerformer(SongPerformer newPerformer);
        void DeletePerformer(SongPerformer deletedPerformer);
        void UpdatePerformer(SongPerformer updatedPerformer);
        SongPerformer GetPerformerById(int? id);
        ICollection<SongPerformer> GetPerformers();
        ICollection<PerformerSong> SongsOfPerformer(SongPerformer performer);
        IEnumerable<SelectListItem> GetPerformersToViews();

        void AddSong(Song newSong, List<SongPerformerViewModel> performers);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong, List<SongPerformerViewModel> performers);
        Song GetSongById(int? id);
        ICollection<Song> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<SongPerformer> GetPerformersOfSong(Song song);

        int GetAlbumCount();
        int GetPerformerCount();
        int GetSongCount();
    }
}
