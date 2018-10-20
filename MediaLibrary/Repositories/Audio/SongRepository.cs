using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Repositories.Audio
{
    public class SongRepository : ISongRepository
    {
        private ApplicationDbContext _context;

        public SongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddSong(Song newSong)
        {
            _context.Songs.Add(newSong);
            _context.SaveChanges();
        }

        public void DeleteSong(Song deletedSong)
        {
            _context.Songs.Remove(deletedSong);
            _context.SaveChanges();
        }

        public ICollection<Album> GetAlbumsOfSong(Song song)
        {
            var als = _context.AlbumSongs.Include(a => a.Album).ThenInclude(al => al.AlbumFormat).Where(a => a.Song == song);
            ICollection<Album> albumlist = new List<Album>();
            foreach (var item in als)
            {
                albumlist.Add(item.Album);
            }
            return albumlist;
        }

        public ICollection<Performer> GetPerformersOfSong(Song song)
        {
            var perfsongs = _context.PerformerSongs.Include(p => p.Performer).Where(ps => ps.Song == song);
            ICollection<Performer> performerlist = new List<Performer>();
            foreach (var item in perfsongs)
            {
                performerlist.Add(item.Performer);
            }
            return performerlist;
        }

        public Song GetSongById(int? id)
        {
            return _context.Songs.Where(s => s.SongID == id).DefaultIfEmpty(null).Single();
        }

        public ICollection<Song> GetSongs()
        {
            return _context.Songs.ToList();
        }

        public void UpdateSong(Song updatedSong)
        {
            _context.Songs.Update(updatedSong);
            _context.SaveChanges();
        }
    }
}
