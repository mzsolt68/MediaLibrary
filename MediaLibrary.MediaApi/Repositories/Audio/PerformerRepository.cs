using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.MediaApi.Interfaces;
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

        public void AddPerformer(Performer newPerformer)
        {
            _context.Performers.Add(newPerformer);
            _context.SaveChanges();
        }

        public void DeletePerformer(Performer deletedPerformer)
        {
            _context.Performers.Remove(deletedPerformer);
            _context.SaveChanges();
        }

        public async Task<Performer> GetPerformerById(int? id)
        {
            return await _context.Performers
                .Include(s => s.PerformerSongs). ThenInclude(ps => ps.Song)
                .Where(p => p.PerformerID == id).SingleOrDefaultAsync();
        }

        public int GetPerformerCount()
        {
            return _context.Performers.Count();
        }

        public async Task<ICollection<Performer>> GetPerformers()
        {
            return await _context.Performers.AsNoTracking().ToListAsync();
        }

        public ICollection<PerformerSong> SongsOfPerformer(Performer performer)
        {
            var pslist = _context.PerformerSongs.Include(x => x.Song).ThenInclude(sa => sa.AlbumSongs).Where(ps => ps.Performer == performer).ToList();
            ICollection<Song> songlist = new List<Song>();
            foreach (var item in pslist)
            {
                item.Song.AlbumSongs = _context.AlbumSongs.Include(als => als.Album).Where(s => s.Song == item.Song).ToList();
            }
            return pslist;
        }

        public void UpdatePerformer(Performer updatedPerformer)
        {
            _context.Performers.Update(updatedPerformer);
            _context.SaveChanges();
        }
    }
}
