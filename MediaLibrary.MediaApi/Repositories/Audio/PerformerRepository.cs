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

        public void AddPerformer(SongPerformer newPerformer)
        {
            _context.SongPerformers.Add(newPerformer);
            _context.SaveChanges();
        }

        public void DeletePerformer(SongPerformer deletedPerformer)
        {
            _context.SongPerformers.Remove(deletedPerformer);
            _context.SaveChanges();
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

        public ICollection<PerformerSong> SongsOfPerformer(SongPerformer performer)
        {
            var pslist = _context.PerformerSongs.Include(x => x.Song).ThenInclude(sa => sa.AlbumSongs).Where(ps => ps.Performer == performer).ToList();
            ICollection<Song> songlist = new List<Song>();
            foreach (var item in pslist)
            {
                item.Song.AlbumSongs = _context.AlbumSongs.Include(als => als.Album).Where(s => s.Song == item.Song).ToList();
            }
            return pslist;
        }

        public void UpdatePerformer(SongPerformer updatedPerformer)
        {
            _context.SongPerformers.Update(updatedPerformer);
            _context.SaveChanges();
        }
    }
}
