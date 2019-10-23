using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Models.Audio;
using MediaLibrary.Models.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<AudioFormat> AudioFormats { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PerformerSong> PerformerSongs { get; set; }
        public DbSet<AlbumSong> AlbumSongs { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            FillAudioTestData.MakeManyToManyConnections(builder);
            FillAudioTestData.FillAudioFormats(builder);
            FillAudioTestData.FillPerformers(builder);
            FillAudioTestData.FillAlbums(builder);
            FillAudioTestData.FillSongs(builder);
        }
    }
}
