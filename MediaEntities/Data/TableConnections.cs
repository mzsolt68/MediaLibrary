using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Entities.Models.Books;
using MediaLibrary.Entities.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Entities.Data
{
    public static class TableConnections
    {
        public static void AudioTableConnections(ModelBuilder builder)
        {
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

        public static void BookTableConnections(ModelBuilder builder)
        {
            builder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorID, ab.BookID });
            builder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(ab => ab.AuthorID);
            builder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ab => ab.BookID);

            builder.Entity<BookTag>()
                .HasKey(bt => new { bt.BookID, bt.TagID });
            builder.Entity<BookTag>()
                .HasOne(bt => bt.Book)
                .WithMany(t => t.Tags)
                .HasForeignKey(bt => bt.BookID);
            builder.Entity<BookTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(b => b.BooksOfTag)
                .HasForeignKey(bt => bt.TagID);

            builder.Entity<FormatBook>()
                .HasKey(fb => new { fb.BookID, fb.FormatID });
            builder.Entity<FormatBook>()
                .HasOne(fb => fb.Book)
                .WithMany(f => f.Formats)
                .HasForeignKey(fb => fb.BookID);
            builder.Entity<FormatBook>()
                .HasOne(fb => fb.Format)
                .WithMany(b => b.BooksInFormat)
                .HasForeignKey(fb => fb.FormatID);
        }
    }
}
