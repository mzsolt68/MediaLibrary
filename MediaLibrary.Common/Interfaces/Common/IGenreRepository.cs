using MediaLibrary.Entities.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Common
{
    interface IGenreRepository
    {
        Task<Genre> AddGenre(Genre newGenre);
        Task<int> DeleteGenre(int? id);
        Task<Genre> UpdateGenre(Genre updatedGenre);
        Task<ICollection<Genre>> GetGenres();
        Task<ICollection<Genre>> GetAudioGenres();
        Task<ICollection<Genre>> GetVideoGenres();
    }
}
