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
    public class AudioFormatRepository : IAudioFormatRepository
    {
        private readonly ApplicationDbContext _context;

        public AudioFormatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AudioFormat> AddFormat(AudioFormat newFormat)
        {
            if (!await _context.AudioFormats.Where(f => f.AudioFormatName.ToLower() == newFormat.AudioFormatName.ToLower()).AnyAsync())
            {
                _context.AudioFormats.Add(newFormat);
                await _context.SaveChangesAsync();
                return newFormat;
            }
            return null;
        }

        public void DeleteFormat(AudioFormat deletedFormat)
        {
            _context.AudioFormats.Remove(deletedFormat);
            _context.SaveChanges();
        }

        public async Task<AudioFormat> GetFormatById(int? id)
        {
            return await _context.AudioFormats.Where(af => af.AudioFormatID == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<ICollection<AudioFormat>> GetFormats()
        {
            return await _context.AudioFormats.AsNoTracking().ToListAsync();
        }

        public void UpdateFormat(AudioFormat updatedFormat)
        {
            _context.AudioFormats.Update(updatedFormat);
            _context.SaveChanges();
        }
    }
}
