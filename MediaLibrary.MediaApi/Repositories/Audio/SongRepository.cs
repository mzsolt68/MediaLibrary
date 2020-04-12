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
        private readonly ApplicationDbContext _context;

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

        public async Task<int> DeleteSong(int? id)
        {
            var deleted = await _context.Songs.Where(s => s.SongID == id).SingleOrDefaultAsync();
            _context.Songs.Remove(deleted);
            int result = await _context.SaveChangesAsync();
            return result;
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
            var song = await _context.Songs
                .Include(p => p.PerformerSongs).ThenInclude(ps => ps.Performer)
                .Include(a => a.AlbumSongs).ThenInclude(als => als.Album)
                .Include(g => g.Genre).Include(l => l.Language)
                .Where(s => s.SongID == id).AsNoTracking().SingleOrDefaultAsync();
            return song;
        }

        public async Task<int> GetSongCount()
        {
            return await _context.Songs.CountAsync();
        }

        public async Task<ICollection<Song>> GetSongs()
        {
            return await _context.Songs
                .Include(s => s.PerformerSongs).ThenInclude(ps => ps.Performer)
                .AsNoTracking().ToListAsync();
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
            song.SongLyric = updatedSong.SongLyric;
            _context.Songs.Update(song);
            _context.SaveChanges();
        }
    }
}
