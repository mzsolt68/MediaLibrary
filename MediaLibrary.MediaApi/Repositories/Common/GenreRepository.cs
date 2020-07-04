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

        public Task<Genre> AddGenre(Genre newGenre)
        {
            throw new NotImplementedException();
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
            var result = await _context.Genres.ToListAsync();
            return result;
        }

        public Task<ICollection<Genre>> GetVideoGenres()
        {
            throw new NotImplementedException();
        }

        public Task<Genre> UpdateGenre(Genre updatedGenre)
        {
            throw new NotImplementedException();
        }
    }
}
