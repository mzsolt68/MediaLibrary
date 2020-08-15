using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Common
{
    public static class ConvertObjects
    {
        public static AlbumDto ConvertAlbumToDto(Album album)
        {
            if (album != null)
            {
                var result = new AlbumDto
                {
                    AlbumID = album.AlbumID,
                    Title = album.AlbumTitle,
                    Format = album.AlbumFormat,
                    Nr_of_discs = album.NrOfDiscs,
                    Nr_of_tracks = album.NrOfSongs
                };
                return result;
            }
            return null;
        }

        public static Album ConvertDtoToAlbum(AlbumDto albumObject)
        {
            Album a = new Album
            {
                AlbumID = albumObject.AlbumID,
                AlbumTitle = albumObject.Title,
                AudioFormatID = albumObject.Format.AudioFormatID,
                NrOfDiscs = (byte)albumObject.Nr_of_discs
            };
            return a;
        }

        public static SongPerformerDto ConvertPerformerToDto(SongPerformer performer)
        {
            var result = new SongPerformerDto
            {
                PerformerID = performer.PerformerID,
                Name = performer.PerformerName
            };
            return result;
        }

        public static SongDto ConvertSongToDto(Song song, bool addPerformers = true)
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
                result.Performers = new List<SongPerformerDto>();
                foreach (var performer in song.PerformerSongs)
                {
                    SongPerformerDto sp = new SongPerformerDto
                    {
                        PerformerID = performer.PerformerID,
                        Name = performer.Performer.PerformerName
                    };
                    result.Performers.Add(sp);
                }
            }
            return result;
        }

        public static void ConvertDtoToSong(SongDto songObject, out Song song, out ICollection<int> performers)
        {
            song = new Song
            {
                SongID = songObject.SongID,
                SongTitle = songObject.Title,
                SongLyric = songObject.Lyric,
                GenreID = songObject.Genre.GenreID,
                LanguageID = songObject.Language.LanguageID
            };
            performers = new List<int>();
            foreach (var item in songObject.Performers)
            {
                performers.Add(item.PerformerID);
            }
        }

        public static AlbumSong ConvertDtoToAlbumSong(int? albumID, int? disc, AudioTrackDto track)
        {
            AlbumSong result = new AlbumSong()
            {
                AlbumID = (int)albumID,
                SongID = track.SongID,
                Disc = (byte)disc,
                TrackNr = track.TrackNr,
                Note = track.Note,
                PlayTime = Convert.ToDateTime(track.PlayTime)
            };
            return result;
        }

        public static AudioTrackDto ConvertAldumSongToDto(AlbumSong albumSong)
        {
            AudioTrackDto newTrack = new AudioTrackDto()
            {
                SongID = albumSong.SongID,
                Title = albumSong.Song.SongTitle,
                TrackNr = albumSong.TrackNr,
                Note = albumSong.Note,
                PlayTime = albumSong.PlayTime.Hour.ToString() + ":" + albumSong.PlayTime.Minute.ToString(),
                Performer = new List<string>()
            };
            foreach (var perfsong in albumSong.Song.PerformerSongs)
            {
                newTrack.Performer.Add(perfsong.Performer.PerformerName);
            }
            return newTrack;
        }
    }
}
