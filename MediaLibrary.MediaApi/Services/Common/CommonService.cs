using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Entities.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Services.Common
{
    public class CommonService : ICommonService
    {
        public Task<Genre> AddGenre(Genre newGenre)
        {
            throw new NotImplementedException();
        }

        public Task<Language> AddLanguage(Language newLanguage)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> AddTag(Tag newTag)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteGenre(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteLanguage(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteTag(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Genre>> GetAudioGenres()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Genre>> GetGenres()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Language>> GetLanguages()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Tag>> GetTags()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Genre>> GetVideoGenres()
        {
            throw new NotImplementedException();
        }

        public Task<Genre> UpdateGenre(Genre updatedGenre)
        {
            throw new NotImplementedException();
        }

        public Task<Language> UpdateLanguage(Language updatedLanguage)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateTag(Tag updatedTag)
        {
            throw new NotImplementedException();
        }
    }
}
