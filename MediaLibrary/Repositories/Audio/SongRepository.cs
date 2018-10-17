using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;

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

        public List<Album> GetAlbumsOfSong(Song song)
        {
            var als = _context.AlbumSongs.Where(a => a.Song == song);
            List<Album> albumlist = new List<Album>();
            foreach (var item in als)
            {
                albumlist.Add(item.Album);
            }
            return albumlist;
        }

        public List<Performer> GetPerformersOfSong(Song song)
        {
            var perfsongs = _context.PerformerSongs.Where(ps => ps.Song == song);
            List<Performer> performerlist = new List<Performer>();
            foreach (var item in perfsongs)
            {
                performerlist.Add(item.Performer);
            }
            return performerlist;
        }

        public Song GetSongById(int id)
        {
            return _context.Songs.Where(s => s.SongID == id).DefaultIfEmpty(null).Single();
        }

        public List<Song> GetSongs()
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
