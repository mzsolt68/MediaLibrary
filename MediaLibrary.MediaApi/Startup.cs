using MediaLibrary.Entities.Data;
using MediaLibrary.Common.Interfaces.Audio;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Repositories.Audio;
using MediaLibrary.Services.Audio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.Repositories.Common;
using MediaLibrary.Services.Common;
using MediaLibrary.Services.Books;
using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Repositories.Books;

namespace MediaLibrary.MediaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IAudioFormatRepository, AudioFormatRepository>();
            services.AddTransient<IPerformerReopsitory, PerformerRepository>();
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookFormatRepository, BookFormatRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IAudioService, AudioService>();
            services.AddTransient<IBookService, BookService>();
            services.AddControllers().AddJsonOptions(
                options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
