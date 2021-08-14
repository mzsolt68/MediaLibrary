using MediaLibrary.Common.Interfaces.Audio;
using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Repositories.Audio;
using MediaLibrary.Repositories.Books;
using MediaLibrary.Repositories.Common;
using MediaLibrary.Services.Audio;
using MediaLibrary.Services.Books;
using MediaLibrary.Services.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MediaLibrary.MediaApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAudioServices(this IServiceCollection services)
        {
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IAudioFormatRepository, AudioFormatRepository>();
            services.AddTransient<IPerformerReopsitory, PerformerRepository>();
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IAudioService, AudioService>();
            return services;
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<ICommonService, CommonService>();
            return services;
        }

        public static IServiceCollection AddBookServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookFormatRepository, BookFormatRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IBookService, BookService>();
            return services;
        }
    }
}
