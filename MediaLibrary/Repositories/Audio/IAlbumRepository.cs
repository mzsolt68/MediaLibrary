using MediaLibrary.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Audio
{
    public interface IAlbumRepository
    {
        void AddAlbum(Album newAlbum);
        void DeleteAlbum(Album deletedAlbum);
        void UpdateAlbum(Album updatedAlbum);
        Album GetAlbumById(int? id);
        List<Album> GetAlbums();
        List<Song> GetSongsOfAlbum(Album album);
    }
}
