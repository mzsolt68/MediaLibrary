using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Dto.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Services
{
    public interface IAudioService
    {
        Task<AlbumDto> AddAlbum(AlbumDto newAlbum);
        Task<int> DeleteAlbum(int? id);
        void UpdateAlbum(Album updatedAlbum);
        Task<AlbumDetailsDto> GetAlbumById(int? id);
        Task<ICollection<AlbumDto>> GetAlbums();
        Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album);

        Task<AudioFormat> AddFormat(AudioFormat newFormat);
        Task<int> DeleteFormat(int? id);
        Task<AudioFormat> UpdateFormat(AudioFormat updatedFormat);
        Task<AudioFormat> GetFormatById(int? id);
        Task<ICollection<AudioFormat>> GetFormats();
        //IEnumerable<SelectListItem> GetFormatsToViews();

        Task<SongPerformerDto> AddPerformer(SongPerformerDto newPerformer);
        Task<int> DeletePerformer(int? id);
        Task<SongPerformerDto> UpdatePerformer(SongPerformerDto updatedPerformer);
        Task<PerformerDetailsDto> GetPerformerById(int? id);
        Task<ICollection<SongPerformerDto>> GetPerformers();
        //IEnumerable<SelectListItem> GetPerformersToViews();

        Task<SongDto> AddSong(SongDto newSong);
        Task<int> DeleteSong(int? id);
        void UpdateSong(Song updatedSong, List<SongPerformerDto> performers);
        Task<SongDetailsDto> GetSongById(int? id);
        Task<ICollection<SongDto>> GetSongs();
        ICollection<Album> GetAlbumsOfSong(Song song);
        ICollection<SongPerformer> GetPerformersOfSong(Song song);

        Task<int> GetAlbumCount();
        Task<int> GetPerformerCount();
        Task<int> GetSongCount();
    }
}
