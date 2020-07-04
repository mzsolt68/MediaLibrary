using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Repositories.Common
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddGenre(Genre newGenre)
        {
            if(!await _context.Genres.Where(g => g.GenreName.ToLower() == newGenre.GenreName.ToLower()).AnyAsync())
            {
                _context.Genres.Add(newGenre);
                await _context.SaveChangesAsync();
                return newGenre;
            }
            return null;
        }

        public Task<int> DeleteGenre(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Genre>> GetAudioGenres()
        {
            var result = await _context.Genres
                .Where(g => g.GenreType == "audio")
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            var result = await _context.Genres.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<ICollection<Genre>> GetVideoGenres()
        {
            var result = await _context.Genres
                .Where(g => g.GenreType == "video")
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<Genre> UpdateGenre(Genre updatedGenre)
        {
            var dbGenre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreID == updatedGenre.GenreID);
            if(dbGenre != null)
            {
                dbGenre.GenreName = updatedGenre.GenreName;
                dbGenre.GenreType = updatedGenre.GenreType;
                await _context.SaveChangesAsync();
            }
            return dbGenre;
        }
    }
}
