using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Entities.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Common
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

        public async Task<int> DeleteGenre(int? id)
        {
            //TODO filmeknél eltávolítani a törölt múfaj hivatkozásokat
            int result = -1;
            if (!await _context.Songs.AnyAsync(s => s.GenreID == id))
            {
                var deleted = await _context.Genres.SingleOrDefaultAsync(g => g.GenreID == id);
                if (deleted != null)
                {
                        _context.Genres.Remove(deleted);
                        result = await _context.SaveChangesAsync();
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        public async Task<ICollection<Genre>> GetAudioGenres()
        {
            var result = await _context.Genres
                .Where(g => g.GenreType == "audio")
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<Genre> GetGenreById(int? id)
        {
            return await _context.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.GenreID == id);
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            var result = await _context.Genres.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<ICollection<Song>> GetSongsByGenre(int? id)
        {
            ICollection<Song> songs = null;
            if(await _context.Genres.AnyAsync(g => g.GenreID == id))
            {
                songs = await _context.Songs
                    .Include(s => s.Performers)
                    .ThenInclude(ps => ps.Performer)
                    .Where(s => s.GenreID == id)
                    .AsNoTracking()
                    .ToListAsync();
            }
            return songs;
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
