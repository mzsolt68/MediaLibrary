namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a unit of work that encapsulates a set of repositories and provides a mechanism to save changes.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository for managing genres.
        /// </summary>
        IGenreRepository GenreRepository { get; }

        /// <summary>
        /// Gets the repository for managing tags.
        /// </summary>
        ITagRepository TagRepository { get; }

        /// <summary>
        /// Gets the repository for managing languages.
        /// </summary>
        ILanguageRepository LanguageRepository { get; }

        /// <summary>
        /// Gets the repository for managing books.
        /// </summary>
        IBookRepository BookRepository { get; }

        /// <summary>
        /// Gets the repository for managing book formats.
        /// </summary>
        IBookFormatRepository BookFormatRepository { get; }

        /// <summary>
        /// Gets the repository for managing authors.
        /// </summary>
        IAuthorRepository AuthorRepository { get; }

        /// <summary>
        /// Gets the repository for managing publishers.
        /// </summary>
        IPublisherRepository PublisherRepository { get; }

        /// <summary>
        /// Saves all changes made in the unit of work to the underlying data store asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
