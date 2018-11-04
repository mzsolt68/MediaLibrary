using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Models.Audio;
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

            builder.Entity<AudioFormat>().HasData(
                new AudioFormat { AudioFormatID = 1, AudioFormatName = "Audio CD"},
                new AudioFormat { AudioFormatID = 2, AudioFormatName = "MP3" },
                new AudioFormat { AudioFormatID = 3, AudioFormatName = "FLAC" },
                new AudioFormat { AudioFormatID = 4, AudioFormatName = "WMA" },
                new AudioFormat { AudioFormatID = 5, AudioFormatName = "WAV" },
                new AudioFormat { AudioFormatID = 6, AudioFormatName = "OGG" }
                );
            builder.Entity<Performer>().HasData(
                new Performer { PerformerID = 1, PerformerName = "Boney M"},
                new Performer { PerformerID = 2, PerformerName = "Jamie Winchester" },
                new Performer { PerformerID = 3, PerformerName = "Hrutka Róbert" },
                new Performer { PerformerID = 4, PerformerName = "Bery" },
                new Performer { PerformerID = 5, PerformerName = "Váczi Eszter" }
                );
            
            builder.Entity<Album>().HasData(
                new { AlbumID = 1, AlbumTitle = "Boney M Gold", AlbumFormatAudioFormatID = 1, NrOfDiscs = (Byte)1},
                new { AlbumID = 2, AlbumTitle = "Bravissimo 8", AlbumFormatAudioFormatID = 1, NrOfDiscs = (Byte)1 },
                new { AlbumID = 3, AlbumTitle = "Bravissimo 6", AlbumFormatAudioFormatID = 1, NrOfDiscs = (Byte)1 },
                new { AlbumID = 4, AlbumTitle = "Vegyes", AlbumFormatAudioFormatID = 1, NrOfDiscs = (Byte)1 }
                );
            builder.Entity<Song>().HasData(
                new Song { SongID = 1, SongTitle = "Rivers of Babylon"},
                new Song { SongID = 2, SongTitle = "Daddy Cool" },
                new Song { SongID = 3, SongTitle = "Sunny" },
                new Song { SongID = 4, SongTitle = "Brown Girl in the Ring" },
                new Song { SongID = 5, SongTitle = "Rasputin" },
                new Song { SongID = 6, SongTitle = "Ma Baker" },
                new Song { SongID = 7, SongTitle = "Hooray! Hooray! It's A Holi-holiday" },
                new Song { SongID = 8, SongTitle = "Painter Man" },
                new Song { SongID = 9, SongTitle = "Belfast" },
                new Song { SongID = 10, SongTitle = "No Woman, No Cry" },
                new Song { SongID = 11, SongTitle = "Mary's Boy Child / Oh My Lord" },
                new Song { SongID = 12, SongTitle = "Gotta Go Home" },
                new Song { SongID = 13, SongTitle = "Still I'm Sad" },
                new Song { SongID = 14, SongTitle = "Nightflight to Venus" },
                new Song { SongID = 15, SongTitle = "Felicidad" },
                new Song { SongID = 16, SongTitle = "El Lute" },
                new Song { SongID = 17, SongTitle = "Baby Do You Wanna Bump" },
                new Song { SongID = 18, SongTitle = "Kalimba De Luna" },
                new Song { SongID = 19, SongTitle = "Happy Song" },
                new Song { SongID = 20, SongTitle = "Megamix" },
                new Song { SongID = 21, SongTitle = "It's Your Life" },
                new Song { SongID = 22, SongTitle = "Egyedül" }
                );
            builder.Entity<PerformerSong>().HasData(
                new PerformerSong { PerformerID = 1, SongID = 1},
                new PerformerSong { PerformerID = 1, SongID = 2 },
                new PerformerSong { PerformerID = 1, SongID = 3 },
                new PerformerSong { PerformerID = 1, SongID = 4 },
                new PerformerSong { PerformerID = 1, SongID = 5 },
                new PerformerSong { PerformerID = 1, SongID = 6 },
                new PerformerSong { PerformerID = 1, SongID = 7 },
                new PerformerSong { PerformerID = 1, SongID = 8 },
                new PerformerSong { PerformerID = 1, SongID = 9 },
                new PerformerSong { PerformerID = 1, SongID = 10 },
                new PerformerSong { PerformerID = 1, SongID = 11 },
                new PerformerSong { PerformerID = 1, SongID = 12 },
                new PerformerSong { PerformerID = 1, SongID = 13 },
                new PerformerSong { PerformerID = 1, SongID = 14 },
                new PerformerSong { PerformerID = 1, SongID = 15 },
                new PerformerSong { PerformerID = 1, SongID = 16 },
                new PerformerSong { PerformerID = 1, SongID = 17 },
                new PerformerSong { PerformerID = 1, SongID = 18 },
                new PerformerSong { PerformerID = 1, SongID = 19 },
                new PerformerSong { PerformerID = 1, SongID = 20 },
                new PerformerSong { PerformerID = 2, SongID = 21 },
                new PerformerSong { PerformerID = 3, SongID = 21 },
                new PerformerSong { PerformerID = 4, SongID = 22 },
                new PerformerSong { PerformerID = 5, SongID = 22 }
                );
            builder.Entity<AlbumSong>().HasData(
                new AlbumSong { AlbumID = 1, SongID = 1, PlayTime = Convert.ToDateTime("4:15"), Disc = 1},
                new AlbumSong { AlbumID = 1, SongID = 2, PlayTime = Convert.ToDateTime("3:26"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 3, PlayTime = Convert.ToDateTime("3:56"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 4, PlayTime = Convert.ToDateTime("4:00"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 5, PlayTime = Convert.ToDateTime("4:24"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 6, PlayTime = Convert.ToDateTime("4:05"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 7, PlayTime = Convert.ToDateTime("3:55"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 8, PlayTime = Convert.ToDateTime("3:16"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 9, PlayTime = Convert.ToDateTime("3:25"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 10, PlayTime = Convert.ToDateTime("4:20"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 11, PlayTime = Convert.ToDateTime("4:01"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 12, PlayTime = Convert.ToDateTime("2:30"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 13, PlayTime = Convert.ToDateTime("4:24"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 14, PlayTime = Convert.ToDateTime("3:49"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 15, PlayTime = Convert.ToDateTime("2:50"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 16, PlayTime = Convert.ToDateTime("3:58"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 17, PlayTime = Convert.ToDateTime("2:25"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 18, PlayTime = Convert.ToDateTime("4:11"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 19, PlayTime = Convert.ToDateTime("3:56"), Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 20, PlayTime = Convert.ToDateTime("3:51"), Disc = 1 },
                new AlbumSong { AlbumID = 2, SongID = 21, PlayTime = Convert.ToDateTime("3:52"), Disc = 1 },
                new AlbumSong { AlbumID = 2, SongID = 22, PlayTime = Convert.ToDateTime("3:45"), Disc = 1 },
                new AlbumSong { AlbumID = 4, SongID = 22, PlayTime = Convert.ToDateTime("3:45"), Disc = 1 }
                );
        }
    }
}
