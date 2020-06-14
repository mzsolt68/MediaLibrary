using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Audio;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.MediaApi.Repositories.Audio
{
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

        //TODO vizsgálni, hogy vannak-e zenék rendelve az előadóhoz
        public async Task<int> DeletePerformer(int? id)
        {
            var deleted = await _context.SongPerformers.SingleOrDefaultAsync(p => p.PerformerID == id);
            if (deleted != null)
            {
                _context.SongPerformers.Remove(deleted);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<SongPerformer> GetPerformerById(int? id)
        {
            return await _context.SongPerformers
                .Include(s => s.PerformerSongs). ThenInclude(ps => ps.Song)
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
    }
}
