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
        Task<AlbumDto> UpdateAlbum(AlbumDto updatedAlbum);
        Task<AlbumDetailsDto> GetAlbumById(int? id);
        Task<ICollection<AlbumDto>> GetAlbums();

        Task<AudioFormat> AddFormat(AudioFormat newFormat);
        Task<int> DeleteFormat(int? id);
        Task<AudioFormat> UpdateFormat(AudioFormat updatedFormat);
        Task<AudioFormat> GetFormatById(int? id);
        Task<ICollection<AudioFormat>> GetFormats();

        Task<SongPerformerDto> AddPerformer(SongPerformerDto newPerformer);
        Task<int> DeletePerformer(int? id);
        Task<SongPerformerDto> UpdatePerformer(SongPerformerDto updatedPerformer);
        Task<PerformerDetailsDto> GetPerformerById(int? id);
        Task<ICollection<SongPerformerDto>> GetPerformers();
        Task<ICollection<SongDto>> GetSongsOfPerformer(int? performerId);

        Task<SongDto> AddSong(SongDto newSong);
        Task<int> DeleteSong(int? id);
        Task<SongDto> UpdateSong(SongDto updatedSong);
        Task<SongDetailsDto> GetSongById(int? id);
        Task<ICollection<SongDto>> GetSongs();
        Task<ICollection<AlbumDto>> GetAlbumsOfSong(int? songId);
        Task<ICollection<SongPerformerDto>> GetPerformersOfSong(int? songId);

        Task<int> GetAlbumCount();
        Task<int> GetPerformerCount();
        Task<int> GetSongCount();

        Task<AudioTrackDto> AddTrackToAlbum(int? albumID, int? discNr, AudioTrackDto track);
        Task<int> DeleteTrack(int? albumID, int? discNr, int? trackNr);
        Task<AudioTrackDto> UpdateTrack(int? albumID, int? discNr, AudioTrackDto updatedTrack);
        Task<IEnumerable<AudioTrackDto>> UpdateTrackList(int? albumID, int? discNr, IEnumerable<AudioTrackDto> trackList);
    }
}
