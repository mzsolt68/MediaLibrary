using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Repositories.Audio
{
    public class PerformerRepository : IPerformerReopsitory
    {
        private ApplicationDbContext _context;

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

        public SongPerformer GetPerformerById(int? id)
        {
            return _context.SongPerformers.Where(p => p.PerformerID == id).SingleOrDefault();
        }

        public int GetPerformerCount()
        {
            return _context.SongPerformers.Count();
        }

        public ICollection<SongPerformer> GetPerformers()
        {
            return _context.SongPerformers.Include(x => x.PerformerSongs).OrderBy(p => p.PerformerName).ToList();
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
