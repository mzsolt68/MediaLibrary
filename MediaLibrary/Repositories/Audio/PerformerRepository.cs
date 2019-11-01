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

        public Performer GetPerformerById(int? id)
        {
            return _context.Performers.Where(p => p.PerformerID == id).SingleOrDefault();
        }

        public int GetPerformerCount()
        {
            return _context.Performers.Count();
        }

        public ICollection<Performer> GetPerformers()
        {
            return _context.Performers.Include(x => x.PerformerSongs).OrderBy(p => p.PerformerName).ToList();
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
