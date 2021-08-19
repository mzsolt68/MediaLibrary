using MediaLibrary.Entities.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Books
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthor(Author newAuthor);
        Task<Author> UpdateAuthor(Author updatedAuthor);
        Task<int> DeleteAuthor(int? authorID);
        Task<Author> GetAuthorByID(int? authorID);
        Task<ICollection<Author>> GetAuthors();
        Task<Author> GetBooksOfAuthor(int? authorID);
    }
}
