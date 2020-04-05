using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.MediaApi.Interfaces;
using MediaLibrary.Entities.Dto.Audio;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.MediaApi.Repositories.Audio
{
    public class SongRepository : ISongRepository
    {
        private ApplicationDbContext _context;

        public SongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddSong(Song newSong, List<SongPerformerDto> performers)
        {
            _context.Songs.Add(newSong);
            _context.SaveChanges();
            foreach (var item in performers)
            {
                if (item != null)
                {
                    _context.PerformerSongs.Add(
                        new PerformerSong
                        { SongID = newSong.SongID, PerformerID = item.Performer.PerformerID}
                        );
                }
            }
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

        public async Task<Song> GetSongById(int? id)
        {
            return await _context.Songs.Where(s => s.SongID == id).SingleOrDefaultAsync();
        }

        public int GetSongCount()
        {
            return _context.Songs.Count();
        }

        public async Task<ICollection<Song>> GetSongs()
        {
            var songlist = await _context.Songs.ToListAsync(); ;
            foreach (var item in songlist)
            {
                item.PerformerSongs = _context.PerformerSongs.Include(ps => ps.Performer).Where(x => x.SongID == item.SongID).ToList();
                foreach(var item2 in item.PerformerSongs)
                {
                    item2.Song = null;
                    item2.Performer.PerformerSongs = null;
                }
            }
            return songlist;
        }

        public async Task UpdateSong(Song updatedSong, List<SongPerformerDto> performers)
        {
            var song = await GetSongById(updatedSong.SongID);
            var perforig = _context.PerformerSongs.Where(s => s.SongID == song.SongID).ToList();
            ICollection<Performer> perfupd = new List<Performer>();
            foreach (var item in performers)
            {
                if (item != null)
                {
                    perfupd.Add(item.Performer);
                }
            }
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
                    _context.PerformerSongs.Add(
                        new PerformerSong
                        { PerformerID = item.PerformerID, SongID = updatedSong.SongID}
                        );
                }
            }
            song.SongTitle = updatedSong.SongTitle;
            song.SongLiryc = updatedSong.SongLiryc;
            _context.Songs.Update(song);
            _context.SaveChanges();
        }
    }
}
