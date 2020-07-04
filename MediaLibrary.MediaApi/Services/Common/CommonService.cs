using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Common;
using MediaLibrary.MediaApi.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Services.Common
{
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenreRepository _genres;

        public CommonService(ApplicationDbContext context)
        {
            _context = context;
            _genres = new GenreRepository(_context);
        }
        
        #region Genre

        public async Task<Genre> AddGenre(Genre newGenre)
        {
            return await _genres.AddGenre(newGenre);
        }

        public async Task<int> DeleteGenre(int? id)
        {
            return await _genres.DeleteGenre(id);
        }

        public async Task<ICollection<Genre>> GetAudioGenres()
        {
            return await _genres.GetAudioGenres();
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return await _genres.GetGenres();
        }

        public async Task<ICollection<Genre>> GetVideoGenres()
        {
            return await _genres.GetVideoGenres();
        }

        public async Task<Genre> UpdateGenre(Genre updatedGenre)
        {
            return await _genres.UpdateGenre(updatedGenre);
        }

        public async Task<Genre> GetGenreById(int? id)
        {
            return await _genres.GetGenreById(id);
        }

        #endregion

        #region Language

        public Task<Language> AddLanguage(Language newLanguage)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteLanguage(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Language>> GetLanguages()
        {
            throw new NotImplementedException();
        }

        public Task<Language> UpdateLanguage(Language updatedLanguage)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tag

        public Task<Tag> AddTag(Tag newTag)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteTag(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Tag>> GetTags()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateTag(Tag updatedTag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
