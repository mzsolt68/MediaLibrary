using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Models.Audios;
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
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PerformerSong>()
                .HasKey(ps => new { ps.PerformerID, ps.SongID });
            builder.Entity<PerformerSong>()
                .HasOne(ps => ps.Performer)
                .WithMany(p => p.PerformerSongs)
                .HasForeignKey(ps => ps.PerformerID);
            builder.Entity<PerformerSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PerformerSongs)
                .HasForeignKey(ps => ps.SongID);

            builder.Entity<AlbumSong>()
                .HasKey(als => new { als.AlbumID, als.SongID });
            builder.Entity<AlbumSong>()
                .HasOne(als => als.Album)
                .WithMany(a => a.AlbumSongs)
                .HasForeignKey(als => als.AlbumID);
            builder.Entity<AlbumSong>()
                .HasOne(als => als.Song)
                .WithMany(s => s.AlbumSongs)
                .HasForeignKey(als => als.SongID);
        }
    }
}
