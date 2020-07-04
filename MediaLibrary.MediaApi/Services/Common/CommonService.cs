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

        public Task<int> DeleteGenre(int? id)
        {
            throw new NotImplementedException();
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

        public Task<Genre> UpdateGenre(Genre updatedGenre)
        {
            throw new NotImplementedException();
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
