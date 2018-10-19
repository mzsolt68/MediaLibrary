using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;

namespace MediaLibrary.Repositories.Audio
{
    public class AudioFormatRepository : IAudioFormatRepository
    {
        private ApplicationDbContext _context;

        public AudioFormatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFormat(AudioFormat newFormat)
        {
            _context.AudioFormats.Add(newFormat);
            _context.SaveChanges();
        }

        public void DeleteFormat(AudioFormat deletedFormat)
        {
            _context.AudioFormats.Remove(deletedFormat);
            _context.SaveChanges();
        }

        public AudioFormat GetFormatById(int? id)
        {
            return _context.AudioFormats.Where(af => af.AudioFormatID == id).DefaultIfEmpty(null).Single();
        }

        public ICollection<AudioFormat> GetFormats()
        {
            return _context.AudioFormats.ToList();
        }

        public void UpdateFormat(AudioFormat updatedFormat)
        {
            _context.AudioFormats.Update(updatedFormat);
            _context.SaveChanges();
        }
    }
}
