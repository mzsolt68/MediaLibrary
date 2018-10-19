using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Repositories.Audio
{
    public class AlbumRepository : IAlbumRepository
    {
        private ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAlbum(Album newAlbum)
        {
            _context.Albums.Add(newAlbum);
            _context.SaveChanges();
        }

        public void DeleteAlbum(Album deletedAlbum)
        {
            _context.Albums.Remove(deletedAlbum);
            _context.SaveChanges();
        }

        public Album GetAlbumById(int? id)
        {
            return _context.Albums.Where(a => a.AlbumID == id).DefaultIfEmpty(null).Single();
        }

        public ICollection<Album> GetAlbums()
        {
            return _context.Albums.ToList();
        }

        public ICollection<AlbumSong> GetSongsOfAlbum(Album album)
        {
            return _context.AlbumSongs.Include(x => x.Song).ThenInclude(songPerformer => songPerformer.PerformerSongs).Where(als => als.Album == album).ToList();
        }

        public void UpdateAlbum(Album updatedAlbum)
        {
            _context.Albums.Update(updatedAlbum);
            _context.SaveChanges();
        }
    }
}
