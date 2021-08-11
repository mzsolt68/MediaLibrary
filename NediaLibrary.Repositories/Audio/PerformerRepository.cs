using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Audio;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Repositories.Audio
{
    /// <summary>
    /// Repository to manipulate Performer DB objects
    /// </summary>
    public class PerformerRepository : IPerformerReopsitory
    {
        private readonly ApplicationDbContext _context;

        public PerformerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SongPerformer> AddPerformer(SongPerformer newPerformer)
        {
            if (!await _context.SongPerformers.Where(p => p.PerformerName.ToLower() == newPerformer.PerformerName.ToLower()).AnyAsync())
            {
                _context.SongPerformers.Add(newPerformer);
                await _context.SaveChangesAsync();
                return newPerformer;
            }
            return null;
        }

        public async Task<int> DeletePerformer(int? id)
        {
            var deleted = await _context.SongPerformers.SingleOrDefaultAsync(p => p.PerformerID == id);
            if(deleted == null)
            {
                return 0;
            }
            if(await _context.PerformerSongs.CountAsync(ps => ps.PerformerID == id) > 0)
            {
                return -1;
            }
            _context.SongPerformers.Remove(deleted);
            return await _context.SaveChangesAsync();
        }

        public async Task<SongPerformer> GetPerformerById(int? id)
        {
            return await _context.SongPerformers
                .Include(s => s.Songs). ThenInclude(ps => ps.Song)
                .Where(p => p.PerformerID == id).SingleOrDefaultAsync();
        }

        public async Task<int> GetPerformerCount()
        {
            return await _context.SongPerformers.CountAsync();
        }

        public async Task<ICollection<SongPerformer>> GetPerformers()
        {
            return await _context.SongPerformers.AsNoTracking().ToListAsync();
        }

        public async Task<SongPerformer> UpdatePerformer(SongPerformer updatedPerformer)
        {
            var dbPerformer = await _context.SongPerformers.SingleOrDefaultAsync(p => p.PerformerID == updatedPerformer.PerformerID);
            if (dbPerformer != null)
            {
                dbPerformer.PerformerName = updatedPerformer.PerformerName;
                await _context.SaveChangesAsync();
                return updatedPerformer;
            }
            return null;
        }

        public async Task<ICollection<Song>> GetSongsOfPerformer(int? performerId)
        {
            if(await _context.SongPerformers.CountAsync(p => p.PerformerID == performerId) == 0)
            {
                return null;
            }
            var songs = await _context.PerformerSongs
                .Include(ps => ps.Song)
                .ThenInclude(s => s.Genre)
                .Include(ps => ps.Song)
                .ThenInclude(s => s.Language)
                .Where(p => p.PerformerID == performerId)
                .Select(s => s.Song)
                .AsNoTracking()
                .ToListAsync();
            return songs;
        }
    }
}
