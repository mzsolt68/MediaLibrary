using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Dto.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Services
{
    public interface IAudioService
    {
        void AddAlbum(Album newAlbum);
        Task<int> DeleteAlbum(int? id);
        void UpdateAlbum(Album updatedAlbum);
        Task<AlbumDetailsDto> GetAlbumById(int? id);
        Task<ICollection<AlbumDto>> GetAlbums();
        Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album);

        void AddFormat(AudioFormat newFormat);
        void DeleteFormat(AudioFormat deletedFormat);
        void UpdateFormat(AudioFormat updatedFormat);
        Task<AudioFormat> GetFormatById(int? id);
        Task<ICollection<AudioFormat>> GetFormats();
        //IEnumerable<SelectListItem> GetFormatsToViews();

        void AddPerformer(Performer newPerformer);
        void DeletePerformer(Performer deletedPerformer);
        void UpdatePerformer(Performer updatedPerformer);
        Task<PerformerDetailsDto> GetPerformerById(int? id);
        Task<ICollection<PerformerDto>> GetPerformers();
        ICollection<PerformerSong> SongsOfPerformer(Performer performer);
        //IEnumerable<SelectListItem> GetPerformersToViews();

        void AddSong(Song newSong, List<SongPerformerDto> performers);
        Task<int> DeleteSong(int? id);
        void UpdateSong(Song updatedSong, List<SongPerformerDto> performers);
        Task<SongDetailsDto> GetSongById(int? id);
        Task<ICollection<SongDto>> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<Performer> GetPerformersOfSong(Song song);

        Task<int> GetAlbumCount();
        Task<int> GetPerformerCount();
        Task<int> GetSongCount();
    }
}
