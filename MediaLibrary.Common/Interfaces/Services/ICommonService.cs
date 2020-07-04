using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Entities.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Services
{
    public interface ICommonService
    {
        Task<Genre> AddGenre(Genre newGenre);
        Task<int> DeleteGenre(int? id);
        Task<Genre> UpdateGenre(Genre updatedGenre);
        Task<ICollection<Genre>> GetGenres();
        Task<ICollection<Genre>> GetAudioGenres();
        Task<ICollection<Genre>> GetVideoGenres();
        Task<Genre> GetGenreById(int? id);
        Task<ICollection<SongDto>> GetSongsByGenre(int? id);

        Task<Language> AddLanguage(Language newLanguage);
        Task<int> DeleteLanguage(int? id);
        Task<Language> UpdateLanguage(Language updatedLanguage);
        Task<ICollection<Language>> GetLanguages();

        Task<Tag> AddTag(Tag newTag);
        Task<int> DeleteTag(int? id);
        Task<Tag> UpdateTag(Tag updatedTag);
        Task<ICollection<Tag>> GetTags();
    }
}
