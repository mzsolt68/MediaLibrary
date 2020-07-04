using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Common.Interfaces.Audio;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.MediaApi.Repositories.Audio;
using MediaLibrary.MediaApi.Services.Audio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediaLibrary.Common.Interfaces.Common;
using MediaLibrary.MediaApi.Repositories.Common;
using MediaLibrary.MediaApi.Services.Common;

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
            services.AddTransient<IAudioService, AudioService>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<ICommonService, CommonService>();
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
