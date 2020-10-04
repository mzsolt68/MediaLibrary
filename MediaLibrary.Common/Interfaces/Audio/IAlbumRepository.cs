using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Audio
{
    public interface IAlbumRepository
    {
        Task<Album> AddAlbum(Album newAlbum);
        Task<int> DeleteAlbum(int? id);
        Task<Album> UpdateAlbum(Album updatedAlbum);
        Task<Album> GetAlbumById(int? id);
        Task<ICollection<Album>> GetAlbums();
        Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album);
        Task<int> GetSongsOfAlbum(int id);
        Task<int> GetAlbumCount();
        Task<AlbumSong> AddTrack(AlbumSong newTrack);
        Task<int> DeleteTrack(int? albumID, int? discNr, int? trackNr);
        Task<AlbumSong> UpdateTrack(AlbumSong track);
        Task<ICollection<AlbumSong>> UpdateTrackList(ICollection<AlbumSong> trackList);
    }
}
