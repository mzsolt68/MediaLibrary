using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Entities.Dto.Audio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Interfaces
{
    public interface IAudioService
    {
        void AddAlbum(Album newAlbum);
        void DeleteAlbum(Album deletedAlbum);
        void UpdateAlbum(Album updatedAlbum);
        Task<AlbumDetailsDto> GetAlbumById(int? id);
        Task<ICollection<AlbumDto>> GetAlbums();
        Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album);

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

        void AddSong(Song newSong, List<SongPerformerDto> performers);
        void DeleteSong(Song deletedSong);
        void UpdateSong(Song updatedSong, List<SongPerformerDto> performers);
        Task<Song> GetSongById(int? id);
        Task<ICollection<SongDto>> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<Performer> GetPerformersOfSong(Song song);

        Task<int> GetAlbumCount();
        int GetPerformerCount();
        int GetSongCount();
    }
}
