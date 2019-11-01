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
        void DeleteAlbum(Album deletedAlbum);
        void UpdateAlbum(Album updatedAlbum);
        Task<Album> GetAlbumById(int? id);
        Task<ICollection<Album>> GetAlbums();
        Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album);
        Task<int> GetAlbumCount();
    }
}
