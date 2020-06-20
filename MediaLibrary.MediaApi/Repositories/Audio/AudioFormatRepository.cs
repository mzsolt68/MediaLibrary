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

        public async Task<int> DeleteFormat(int? id)
        {
            int result = -1;
            if (!await _context.Albums.AnyAsync(f => f.AudioFormatID == id))
            {
                var deleted = await _context.AudioFormats.Where(f => f.AudioFormatID == id).SingleOrDefaultAsync();
                if (deleted != null)
                {
                    _context.AudioFormats.Remove(deleted);
                    result = await _context.SaveChangesAsync();
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        public async Task<AudioFormat> GetFormatById(int? id)
        {
            return await _context.AudioFormats.Where(af => af.AudioFormatID == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<ICollection<AudioFormat>> GetFormats()
        {
            return await _context.AudioFormats.AsNoTracking().ToListAsync();
        }

        public async Task<AudioFormat> UpdateFormat(AudioFormat updatedFormat)
        {
            var dbFormat = await _context.AudioFormats.Where(f => f.AudioFormatID == updatedFormat.AudioFormatID).FirstOrDefaultAsync();
            if (dbFormat != null)
            {
                dbFormat.AudioFormatName = updatedFormat.AudioFormatName;
                await _context.SaveChangesAsync();
                return updatedFormat;
            }
            return null;
        }
    }
}
