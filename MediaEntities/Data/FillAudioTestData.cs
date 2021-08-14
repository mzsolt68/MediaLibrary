using System;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Entities.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Entities.Data
{
    public static class FillAudioTestData
    {
        public static void Fill(ModelBuilder builder)
        {
            FillAudioFormats(builder);
            FillPerformers(builder);
            FillAlbums(builder);
            FillGenres(builder);
            FillLanguages(builder);
            FillSongs(builder);
        }

        private static void FillAudioFormats(ModelBuilder builder)
        {
            builder.Entity<AudioFormat>().HasData(
                new AudioFormat { AudioFormatID = 1, AudioFormatName = "Audio CD" },
                new AudioFormat { AudioFormatID = 2, AudioFormatName = "MP3" },
                new AudioFormat { AudioFormatID = 3, AudioFormatName = "FLAC" },
                new AudioFormat { AudioFormatID = 4, AudioFormatName = "WMA" },
                new AudioFormat { AudioFormatID = 5, AudioFormatName = "WAV" },
                new AudioFormat { AudioFormatID = 6, AudioFormatName = "OGG" }
            );
        }

        private static void FillPerformers(ModelBuilder builder)
        {
            builder.Entity<SongPerformer>().HasData(
                new SongPerformer { PerformerID = 1, PerformerName = "Boney M" },
                new SongPerformer { PerformerID = 2, PerformerName = "Jamie Winchester" },
                new SongPerformer { PerformerID = 3, PerformerName = "Hrutka Róbert" },
                new SongPerformer { PerformerID = 4, PerformerName = "Bery" },
                new SongPerformer { PerformerID = 5, PerformerName = "Váczi Eszter" }
            );
        }

        private static void FillAlbums(ModelBuilder builder)
        {
            builder.Entity<Album>().HasData(
                new Album { AlbumID = 1, AlbumTitle = "Boney M Gold", AudioFormatID = 1, NrOfDiscs = (Byte)1 },
                new Album { AlbumID = 2, AlbumTitle = "Bravissimo 8", AudioFormatID = 1, NrOfDiscs = (Byte)1 },
                new Album { AlbumID = 3, AlbumTitle = "Bravissimo 6", AudioFormatID = 1, NrOfDiscs = (Byte)1 },
                new Album { AlbumID = 4, AlbumTitle = "Vegyes", AudioFormatID = 1, NrOfDiscs = (Byte)1 }
            );
        }

        private static void FillGenres(ModelBuilder builder)
        {
            builder.Entity<Genre>().HasData(
                new Genre { GenreID = 1, GenreName = "Disco", GenreType = "audio"},
                new Genre { GenreID = 2, GenreName = "Jazz", GenreType = "audio" },
                new Genre { GenreID = 3, GenreName = "Rock", GenreType = "audio" },
                new Genre { GenreID = 4, GenreName = "Pop", GenreType = "audio" },
                new Genre { GenreID = 5, GenreName = "Dráma", GenreType = "video" },
                new Genre { GenreID = 6, GenreName = "Vígjáték", GenreType = "video" },
                new Genre { GenreID = 7, GenreName = "Akció", GenreType = "video" },
                new Genre { GenreID = 8, GenreName = "Romantikus", GenreType = "video" }
            );
        }

        private static void FillLanguages(ModelBuilder builder)
        {
            builder.Entity<Language>().HasData(
                new Language { LanguageID = 1, LanguageName = "angol"},
                new Language { LanguageID = 2, LanguageName = "francia"},
                new Language { LanguageID = 3, LanguageName = "német"},
                new Language { LanguageID = 4, LanguageName = "magyar"}
            );
        }

        private static void FillSongs(ModelBuilder builder)
        {
            builder.Entity<Song>().HasData(
                new Song { SongID = 1, SongTitle = "Rivers of Babylon", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 2, SongTitle = "Daddy Cool", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 3, SongTitle = "Sunny", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 4, SongTitle = "Brown Girl in the Ring", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 5, SongTitle = "Rasputin", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 6, SongTitle = "Ma Baker", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 7, SongTitle = "Hooray! Hooray! It's A Holi-holiday", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 8, SongTitle = "Painter Man", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 9, SongTitle = "Belfast", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 10, SongTitle = "No Woman, No Cry", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 11, SongTitle = "Mary's Boy Child / Oh My Lord", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 12, SongTitle = "Gotta Go Home", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 13, SongTitle = "Still I'm Sad", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 14, SongTitle = "Nightflight to Venus", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 15, SongTitle = "Felicidad", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 16, SongTitle = "El Lute", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 17, SongTitle = "Baby Do You Wanna Bump", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 18, SongTitle = "Kalimba De Luna", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 19, SongTitle = "Happy Song", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 20, SongTitle = "Megamix", GenreID = 1, LanguageID = 1 },
                new Song { SongID = 21, SongTitle = "It's Your Life", GenreID = 4, LanguageID = 1 },
                new Song { SongID = 22, SongTitle = "Egyedül", GenreID = 4, LanguageID = 4 }
            );
            builder.Entity<PerformerSong>().HasData(
                new PerformerSong { PerformerID = 1, SongID = 1 },
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
                new AlbumSong { AlbumID = 1, SongID = 1, TrackNr = 1, PlayTime = "04:15", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 2, TrackNr = 2, PlayTime = "03:26", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 3, TrackNr = 3, PlayTime = "03:56", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 4, TrackNr = 4, PlayTime = "04:00", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 5, TrackNr = 5, PlayTime = "04:24", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 6, TrackNr = 6, PlayTime = "04:05", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 7, TrackNr = 7, PlayTime = "3:55", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 8, TrackNr = 8, PlayTime = "03:16", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 9, TrackNr = 9, PlayTime = "03:25", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 10, TrackNr = 10, PlayTime = "04:20", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 11, TrackNr = 11, PlayTime = "04:01", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 12, TrackNr = 12, PlayTime = "02:30", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 13, TrackNr = 13, PlayTime = "04:24", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 14, TrackNr = 14, PlayTime = "03:49", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 15, TrackNr = 15, PlayTime = "02:50", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 16, TrackNr = 16, PlayTime = "03:58", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 17, TrackNr = 17, PlayTime = "02:25", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 18, TrackNr = 18, PlayTime = "04:11", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 19, TrackNr = 19, PlayTime = "03:56", Disc = 1 },
                new AlbumSong { AlbumID = 1, SongID = 20, TrackNr = 20, PlayTime = "03:51", Disc = 1 },
                new AlbumSong { AlbumID = 2, SongID = 21, TrackNr = 20, PlayTime = "03:52", Disc = 1 },
                new AlbumSong { AlbumID = 2, SongID = 22, TrackNr = 1, PlayTime = "03:45", Disc = 1 },
                new AlbumSong { AlbumID = 4, SongID = 22, TrackNr = 3, PlayTime = "03:45", Disc = 1 }
            );
        }
    }
}
