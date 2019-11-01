using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.MediaApi.Interfaces;

namespace MediaLibrary.MediaApi.Repositories.Audio
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
            return _context.AudioFormats.Where(af => af.AudioFormatID == id).SingleOrDefault();
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
