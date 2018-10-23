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
            var perfsongs = _context.PerformerSongs.Include(p => p.Performer).Where(ps => ps.Song == song).ToList(); ;
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
            var songlist = _context.Songs.ToList(); ;
            foreach (var item in songlist)
            {
                item.PerformerSongs = _context.PerformerSongs.Include(ps => ps.Performer).Where(x => x.SongID == item.SongID).ToList();
            }
            return songlist;
        }

        public void UpdateSong(Song updatedSong)
        {
            var song = GetSongById(updatedSong.SongID);
            var perforig = _context.PerformerSongs.Where(s => s.SongID == song.SongID).ToList();
            var perfupd = updatedSong.PerformerSongs;
            foreach (var item in perforig)
            {
                if (!perfupd.Select(x => x.PerformerID).ToList().Contains(item.PerformerID))
                {
                    _context.PerformerSongs.Remove(item);
                }
            }
            foreach (var item in perfupd)
            {
                if(!perforig.Select(x => x.PerformerID).ToList().Contains(item.PerformerID))
                {
                    //item.SongID = song.SongID;
                    _context.PerformerSongs.Add(item);
                }
            }
            song.SongTitle = updatedSong.SongTitle;
            song.SongLiryc = updatedSong.SongLiryc;
            _context.Songs.Update(song);
            _context.SaveChanges();
        }
    }
}
