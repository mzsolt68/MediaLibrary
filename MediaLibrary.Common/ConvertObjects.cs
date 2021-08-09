using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaLibrary.Common
{
    public static class ConvertObjects
    {
        /// <summary>
        /// Converts album DB object to DTO
        /// </summary>
        /// <param name="album">Album to convert</param>
        /// <returns>DTO object</returns>
        public static AlbumDto AsAlbumDto(this Album album)
        {
            return new AlbumDto
            {
                AlbumID = album.AlbumID,
                Title = album.AlbumTitle,
                Format = album.AlbumFormat,
                Nr_of_discs = album.NrOfDiscs,
                Nr_of_tracks = album.NrOfSongs
            };
        }

        /// <summary>
        /// Converts album DTO to DB object
        /// </summary>
        /// <param name="albumObject">DTO to convert</param>
        /// <returns>DB object</returns>
        public static Album AsAlbum(this AlbumDto albumObject)
        {
            return new Album
            {
                AlbumID = albumObject.AlbumID,
                AlbumTitle = albumObject.Title,
                AudioFormatID = albumObject.Format.AudioFormatID,
                NrOfDiscs = (byte)albumObject.Nr_of_discs
            };
        }

        /// <summary>
        /// Converts song performer DB object to DTO
        /// </summary>
        /// <param name="performer">Performer to convert</param>
        /// <returns>DTO object</returns>
        public static SongPerformerDto AsSongPerformerDto(this SongPerformer performer)
        {
            return new SongPerformerDto
            {
                PerformerID = performer.PerformerID,
                Name = performer.PerformerName
            };
        }

        /// <summary>
        /// Converts song performer DTO to DB object
        /// </summary>
        /// <param name="albumObject">DTO to convert</param>
        /// <returns>DB object</returns>
        public static SongPerformer AsPerformer(this SongPerformerDto performer)
        {
            return new SongPerformer
            {
                PerformerID = performer.PerformerID,
                PerformerName = performer.Name
            };
        }

        /// <summary>
        /// Converts song DB object to DTO
        /// </summary>
        /// <param name="song">Song to convert</param>
        /// <param name="addPerformers">Need to add performers to DTO?</param>
        /// <returns>DTO object</returns>
        public static SongDto AsSongDto(this Song song, bool addPerformers = true)
        {
            SongDto result = new SongDto
            {
                SongID = song.SongID,
                Title = song.SongTitle,
                Lyric = song.SongLyric,
                Genre = song.Genre,
                Language = song.Language
            };
            if (addPerformers)
            {
                result.Performers = song.PerformerSongs.Select(p => p.Performer.AsSongPerformerDto()).ToList();
            }
            return result;
        }

        /// <summary>
        /// Converts song DTO to DB object
        /// </summary>
        /// <param name="songObject">DTO to convert</param>
        /// <param name="performers">List of performer IDs</param>
        /// <returns>DB object</returns>
        public static Song AsSong(this SongDto songObject, out ICollection<int> performers)
        {
            performers = songObject.Performers.Select(p => p.PerformerID).ToList();
            return new Song
            {
                SongID = songObject.SongID,
                SongTitle = songObject.Title,
                SongLyric = songObject.Lyric,
                GenreID = songObject.Genre.GenreID,
                LanguageID = songObject.Language.LanguageID
            };
        }

        /// <summary>
        /// Converts album track DTO to DB object
        /// </summary>
        /// <param name="track">Track to convert</param>
        /// <param name="albumID">Album ID track belongs</param>
        /// <param name="disc">Disc Nr contains the track</param>
        /// <returns>DB object</returns>
        public static AlbumSong AsAlbumSong(this AudioTrackDto track, int? albumID, int? disc)
        {
            return new AlbumSong()
            {
                AlbumID = (int)albumID,
                SongID = track.SongID,
                Disc = (byte)disc,
                TrackNr = track.TrackNr,
                Note = track.Note,
                PlayTime = track.PlayTime
            };
        }

        /// <summary>
        /// Converts album's song DB object to DTO
        /// </summary>
        /// <param name="albumSong">Song to convert</param>
        /// <returns>DTO object</returns>
        public static AudioTrackDto AsAudioTrackDto(this AlbumSong albumSong)
        {
            return new AudioTrackDto()
            {
                SongID = albumSong.SongID,
                Title = albumSong.Song.SongTitle,
                TrackNr = albumSong.TrackNr,
                Note = albumSong.Note,
                PlayTime = albumSong.PlayTime,
                Performer = albumSong.Song.PerformerSongs.Select(p => p.Performer.PerformerName).ToList()
            };
        }

        /// <summary>
        /// Converts song DB object to DTO
        /// </summary>
        /// <param name="song">Song to convert</param>
        /// <returns>DTo object</returns>
        public static SongDetailsDto AsSongDetailsDto(this Song song)
        {
            var result = new SongDetailsDto()
            {
                Song = new SongDto()
                {
                    SongID = song.SongID,
                    Title = song.SongTitle,
                    Performers = song.PerformerSongs.Select(ps => ps.Performer.AsSongPerformerDto()).ToList()
                },
                Genre = song.Genre != null ? song.Genre.GenreName : "",
                Language = song.Language != null ? song.Language.LanguageName : ""
            };
            if(song.AlbumSongs.Count > 0)
            {
                result.Albums = song.AlbumSongs.Select(a => a.AsAlbumSongDto()).ToList();
            }
            return result;
        }

        /// <summary>
        /// Converts albums's song DB object to AlbumSong DTO
        /// </summary>
        /// <param name="albumSong">Song to convert</param>
        /// <returns>DTO object</returns>
        public static AlbumSongDto AsAlbumSongDto(this AlbumSong albumSong)
        {
            return new AlbumSongDto()
            {
                AlbumID = albumSong.Album.AlbumID,
                Title = albumSong.Album.AlbumTitle,
                Format = albumSong.Album.AlbumFormat,
                TrackNr = albumSong.TrackNr.ToString(),
                PlayTime = albumSong.PlayTime
            };
        }

        /// <summary>
        /// Converts album details DB object to DTO
        /// </summary>
        /// <param name="disc">Album details to convert</param>
        /// <returns>DTO object</returns>
        public static AudioDiscDto AsAudioDiscDto(this IGrouping<byte, AlbumSong> disc)
        {
            return new AudioDiscDto()
            {
                DiscNumber = disc.Key,
                Tracks = disc.Select(s => s.AsAudioTrackDto()).ToList()
            };
        }
    }
}
