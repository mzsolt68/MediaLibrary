using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;

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

        public Performer GetPerformerById(int id)
        {
            return _context.Performers.Where(p => p.PerformerID == id).DefaultIfEmpty(null).Single();
        }

        public List<Performer> GetPerformers()
        {
            return _context.Performers.ToList();
        }

        public List<Song> SongsOfPerformer(Performer performer)
        {
            var pslist = _context.PerformerSongs.Where(ps => ps.Performer == performer);
            List<Song> songlist = new List<Song>();
            foreach (var item in pslist)
            {
                songlist.Add(item.Song);
            }
            return songlist;
        }

        public void UpdatePerformer(Performer updatedPerformer)
        {
            _context.Performers.Update(updatedPerformer);
            _context.SaveChanges();
        }
    }
}
