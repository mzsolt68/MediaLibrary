using MediaEntities.Models.Audio;
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

        void AddPerformer(Performer newPerformer);
        void DeletePerformer(Performer deletedPerformer);
        void UpdatePerformer(Performer updatedPerformer);
        Performer GetPerformerById(int? id);
        ICollection<Performer> GetPerformers();
        ICollection<PerformerSong> SongsOfPerformer(Performer performer);
        IEnumerable<SelectListItem> GetPerformersToViews();

        void AddSong(Song newSong, List<SongPerformerViewModel> performers);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong, List<SongPerformerViewModel> performers);
        Song GetSongById(int? id);
        ICollection<Song> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<Performer> GetPerformersOfSong(Song song);

        int GetAlbumCount();
        int GetPerformerCount();
        int GetSongCount();
    }
}
