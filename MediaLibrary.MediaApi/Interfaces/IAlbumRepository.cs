using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Interfaces
{
    public interface IAlbumRepository
    {
        void AddAlbum(Album newAlbum);
        Task<int> DeleteAlbum(int? id);
        void UpdateAlbum(Album updatedAlbum);
        Task<Album> GetAlbumById(int? id);
        Task<ICollection<Album>> GetAlbums();
        Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album);
        Task<int> GetSongsOfAlbum(int id);
        Task<int> GetAlbumCount();
    }
}
