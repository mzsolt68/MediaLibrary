using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Audio;
using MediaLibrary.Common.Dto.Audio;
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

        public async Task<Song> AddSong(Song newSong, ICollection<int> performers)
        {
            _context.Songs.Add(newSong);
            await _context.SaveChangesAsync();
            foreach (var item in performers)
            {
                    _context.PerformerSongs.Add(
                        new PerformerSong
                        {
                            SongID = newSong.SongID,
                            PerformerID = item
                        });
            }
            await _context.SaveChangesAsync();
            return await _context.Songs
                .Include(s => s.PerformerSongs).ThenInclude(ps => ps.Performer)
                .Include(g => g.Genre)
                .Include(l => l.Language)
                .AsNoTracking().FirstOrDefaultAsync(s => s.SongID == newSong.SongID);
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

        public ICollection<SongPerformer> GetPerformersOfSong(Song song)
        {
            var perfsongs = _context.PerformerSongs.Include(p => p.Performer).Where(ps => ps.Song == song).ToList(); ;
            ICollection<SongPerformer> performerlist = new List<SongPerformer>();
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
                .Include(a => a.AlbumSongs).ThenInclude(als => als.Album).ThenInclude(al => al.AlbumFormat)
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

        public async Task<Song> UpdateSong(Song updatedSong, ICollection<int> performers)
        {
            var song = await _context.Songs.SingleOrDefaultAsync(s => s.SongID == updatedSong.SongID);
            if (song != null)
            {
                var dbperformers = await _context.PerformerSongs.Where(s => s.SongID == song.SongID).ToListAsync();
                ICollection<SongPerformer> updatedPerformers = new List<SongPerformer>();
                foreach (var item in performers)
                {
                    var updPerformer = await _context.SongPerformers.SingleOrDefaultAsync(p => p.PerformerID == item);
                    if (updPerformer != null)
                    {
                        updatedPerformers.Add(updPerformer);
                    }
                }
                foreach (var item in dbperformers)
                {
                    if (!updatedPerformers.Select(x => x.PerformerID).ToList().Contains(item.PerformerID))
                    {
                        _context.PerformerSongs.Remove(item);
                    }
                }
                foreach (var item in updatedPerformers)
                {
                    if (!dbperformers.Select(x => x.PerformerID).ToList().Contains(item.PerformerID))
                    {
                        _context.PerformerSongs.Add(
                            new PerformerSong
                            { PerformerID = item.PerformerID, SongID = updatedSong.SongID }
                            );
                    }
                }
                song.SongTitle = updatedSong.SongTitle;
                song.SongLyric = updatedSong.SongLyric;
                song.GenreID = updatedSong.GenreID;
                song.LanguageID = updatedSong.LanguageID;
                await _context.SaveChangesAsync();
                song = await _context.Songs
                    .Include(ps => ps.PerformerSongs).ThenInclude(p => p.Performer)
                    .Include(g => g.Genre)
                    .Include(l => l.Language)
                    .AsNoTracking().FirstOrDefaultAsync(s => s.SongID == updatedSong.SongID);
            }
            return song;
        }
    }
}
