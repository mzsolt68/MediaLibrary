using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.Entities.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.MediaApi.Repositories.Common
{
    public class GenreRepository : IGenreRepository
    {
        public Task<Genre> AddGenre(Genre newGenre)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteGenre(int? id)
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
