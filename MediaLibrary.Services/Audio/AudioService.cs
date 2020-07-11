using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Common.Interfaces.Audio;
using MediaLibrary.Repositories.Audio;
using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Common;

namespace MediaLibrary.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlbumRepository _albums;
        private readonly IAudioFormatRepository _formats;
        private readonly IPerformerReopsitory _performers;
        private readonly ISongRepository _songs;

        public AudioService(ApplicationDbContext context)
        {
            _context = context;
            _albums = new AlbumRepository(_context);
            _formats = new AudioFormatRepository(_context);
            _performers = new PerformerRepository(_context);
            _songs = new SongRepository(_context);
        }

        #region Album

        public async Task<AlbumDto> AddAlbum(AlbumDto newAlbum)
        {
            var result = await _albums.AddAlbum(ConvertObjects.ConvertDtoToAlbum(newAlbum));
            if(result != null)
            {
                return ConvertObjects.ConvertAlbumToDto(result);
            }
            return null;
        }

        public async Task<int> DeleteAlbum(int? id)
        {
            return await _albums.DeleteAlbum(id);
        }

        public async Task<AlbumDetailsDto> GetAlbumById(int? id)
        {
            AlbumDetailsDto result = null;
            var album = await _albums.GetAlbumById(id);
            if (album != null)
            {
                result = new AlbumDetailsDto
                {
                    Album = ConvertObjects.ConvertAlbumToDto(album)
                };
                result.Discs = new List<AudioDiscDto>();
                var songlist = (await _albums.GetSongsOfAlbum(album)).GroupBy(d => d.Disc);
                foreach (var disc in songlist)
                {
                    AudioDiscDto d = new AudioDiscDto
                    {
                        DiscNumber = disc.Key,
                        Tracks = new List<AudioTrackDto>()
                    };
                    foreach (var song in disc)
                    {
                        AudioTrackDto track = new AudioTrackDto
                        {
                            TrackNr = song.TrackNr,
                            Title = song.Song.SongTitle,
                            PlayTime = song.PlayTime.Hour.ToString() + ":" + song.PlayTime.Minute.ToString(),
                            Note = song.Note,
                            Performer = new List<string>()
                        };
                        foreach (var perfsong in song.Song.PerformerSongs)
                        {
                            track.Performer.Add(perfsong.Performer.PerformerName);
                        }
                        d.Tracks.Add(track);
                    }
                    result.Discs.Add(d);
                }
            }
            return result;
        }

        public async Task<ICollection<AlbumDto>> GetAlbums()
        {
            List<AlbumDto> result = null;
            var albums = await _albums.GetAlbums();
            if (albums.Count > 0)
            {
                result = new List<AlbumDto>();
                foreach (var album in albums)
                {
                    AlbumDto a = ConvertObjects.ConvertAlbumToDto(album);
                    result.Add(a);
                }
            }
            return result;
        }

        public async Task<AlbumDto> UpdateAlbum(AlbumDto updatedAlbum)
        {
            var result = await _albums.UpdateAlbum(ConvertObjects.ConvertDtoToAlbum(updatedAlbum));
            if(result != null)
            {
                return ConvertObjects.ConvertAlbumToDto(result);
            }
            return null;
        }

        public async Task<int> GetAlbumCount()
        {
            return await _albums.GetAlbumCount();
        }

        #endregion

        #region Performer

        public async Task<SongPerformerDto> AddPerformer(SongPerformerDto newPerformer)
        {
            var addedPerformer = new SongPerformer
            {
                PerformerName = newPerformer.Name
            };
            var result = await _performers.AddPerformer(addedPerformer);
            if(result != null)
            {
                return ConvertObjects.ConvertPerformerToDto(result);
            }
            return null;
        }

        public async Task<int> DeletePerformer(int? id)
        {
            return await _performers.DeletePerformer(id);
        }

        public async Task<PerformerDetailsDto> GetPerformerById(int? id)
        {
            PerformerDetailsDto result = null;
            var performer = await _performers.GetPerformerById(id);
            if (performer != null)
            {
                result = new PerformerDetailsDto
                {
                    Performer = new SongPerformerDto
                    {
                        PerformerID = performer.PerformerID,
                        Name = performer.PerformerName
                    },
                };
                if (performer.PerformerSongs.Count > 0)
                {
                    result.Songs = new List<SongDto>();
                    foreach (var song in performer.PerformerSongs)
                    {
                        SongDto s = new SongDto
                        {
                            SongID = song.Song.SongID,
                            Title = song.Song.SongTitle
                        };
                        result.Songs.Add(s);
                    }
                }
            }
            return result;
        }

        public async Task<ICollection<SongPerformerDto>> GetPerformers()
        {
            List<SongPerformerDto> result = null;
            var performers = await _performers.GetPerformers();
            if (performers.Count > 0)
            {
                result = new List<SongPerformerDto>();
                foreach (var performer in performers)
                {
                    SongPerformerDto p = new SongPerformerDto
                    {
                        PerformerID = performer.PerformerID,
                        Name = performer.PerformerName
                    };
                    result.Add(p);
                }
            }
            return result;
        }

        public async Task<SongPerformerDto> UpdatePerformer(SongPerformerDto updatedPerformer)
        {
            var tmp = new SongPerformer
            {
                PerformerID = updatedPerformer.PerformerID,
                PerformerName = updatedPerformer.Name
            };
            var result = await _performers.UpdatePerformer(tmp);
            if(result != null)
            {
                return ConvertObjects.ConvertPerformerToDto(result);
            }
            return null;
        }

        public async Task<int> GetPerformerCount()
        {
            return await _performers.GetPerformerCount();
        }

        public async Task<ICollection<SongDto>> GetSongsOfPerformer(int? performerId)
        {
            if(!performerId.HasValue)
            {
                return null;
            }
            var songs = await _performers.GetSongsOfPerformer(performerId);
            if(songs == null)
            {
                return null;
            }
            var songsOfPerformer = new List<SongDto>();
            foreach (var song in songs)
            {
                songsOfPerformer.Add(ConvertObjects.ConvertSongToDto(song, false));
            }
            return songsOfPerformer;
        }

        #endregion

        #region Song

        public async Task<SongDto> AddSong(SongDto newSong)
        {
            ConvertObjects.ConvertDtoToSong(newSong, out Song s, out ICollection<int> p);
            var result = await _songs.AddSong(s, p);
            if (result != null)
            {
                return ConvertObjects.ConvertSongToDto(result);
            }
            return null;
        }

        public async Task<int> DeleteSong(int? id)
        {
            return await _songs.DeleteSong(id);
        }

        public async Task<SongDetailsDto> GetSongById(int? id)
        {
            SongDetailsDto result = null;
            var song = await _songs.GetSongById(id);
            if (song != null)
            {
                result = new SongDetailsDto
                {
                    Song = new SongDto()
                };
                result.Song.SongID = song.SongID;
                result.Song.Title = song.SongTitle;
                result.Song.Performers = new List<SongPerformerDto>();
                foreach (var perf in song.PerformerSongs)
                {
                    SongPerformerDto p = new SongPerformerDto
                    {
                        PerformerID = perf.Performer.PerformerID,
                        Name = perf.Performer.PerformerName
                    };
                    result.Song.Performers.Add(p);
                }
                if (song.Genre != null)
                {
                    result.Genre = song.Genre.GenreName;
                }
                if (song.Language != null)
                {
                    result.Language = song.Language.LanguageName;
                }
                if (song.AlbumSongs.Count > 0)
                {
                    result.Albums = new List<AlbumSongDto>();
                    foreach (var album in song.AlbumSongs)
                    {
                        AlbumSongDto a = new AlbumSongDto
                        {
                            AlbumID = album.Album.AlbumID,
                            Title = album.Album.AlbumTitle,
                            Format = album.Album.AlbumFormat,
                            TrackNr = album.TrackNr.ToString(),
                            PlayTime = album.PlayTime.Hour.ToString() + ":" + album.PlayTime.Minute.ToString()
                        };
                        result.Albums.Add(a);
                    }
                }
            }
            return result;
        }

        public async Task<ICollection<SongDto>> GetSongs()
        {
            ICollection<SongDto> result = null;
            var songs = await _songs.GetSongs();
            if (songs.Count > 0)
            {
                result = new List<SongDto>();
                foreach (var song in songs)
                {
                    SongDto s = new SongDto
                    {
                        SongID = song.SongID,
                        Title = song.SongTitle,
                        Performers = new List<SongPerformerDto>()
                    };
                    foreach (var perf in song.PerformerSongs)
                    {
                        SongPerformerDto p = new SongPerformerDto
                        {
                            PerformerID = perf.Performer.PerformerID,
                            Name = perf.Performer.PerformerName
                        };
                        s.Performers.Add(p);
                    }
                    result.Add(s);
                }
            }
            return result;
        }

        public async Task<SongDto> UpdateSong(SongDto updatedSong)
        {
            ConvertObjects.ConvertDtoToSong(updatedSong, out Song s, out ICollection<int> p);
            var result = await _songs.UpdateSong(s, p);
            if(result != null)
            {
                return ConvertObjects.ConvertSongToDto(result);
            }
            return null;
        }

        public async Task<int> GetSongCount()
        {
            return await _songs.GetSongCount();
        }

        public async Task<ICollection<SongPerformerDto>> GetPerformersOfSong(int? songId)
        {
            if (!songId.HasValue)
            {
                return null;
            }
            var performers = await _songs.GetPerformersOfSong(songId);
            var performersOfSong = new List<SongPerformerDto>();
            foreach (var performer in performers)
            {
                performersOfSong.Add(ConvertObjects.ConvertPerformerToDto(performer));
            }
            return performersOfSong;
        }

        public async Task<ICollection<AlbumDto>> GetAlbumsOfSong(int? songId)
        {
            if(!songId.HasValue)
            {
                return null;
            }
            var albums = await _songs.GetAlbumsOfSong(songId);
            if(albums == null)
            {
                return null;
            }
            var albumsOfSong = new List<AlbumDto>();
            foreach (var album in albums)
            {
                album.NrOfSongs = await _albums.GetSongsOfAlbum(album.AlbumID);
                albumsOfSong.Add(ConvertObjects.ConvertAlbumToDto(album));
            }
            return albumsOfSong;
        }

        #endregion

        #region Format

        public async Task<AudioFormat> AddFormat(AudioFormat newFormat)
        {
            return await _formats.AddFormat(newFormat);
        }

        public async Task<int> DeleteFormat(int? id)
        {
            return await _formats.DeleteFormat(id);
        }

        public async Task<AudioFormat> GetFormatById(int? id)
        {
            return await _formats.GetFormatById(id);
        }

        public async Task<ICollection<AudioFormat>> GetFormats()
        {
            return await _formats.GetFormats();
        }

        public async Task<AudioFormat> UpdateFormat(AudioFormat updatedFormat)
        {
            return await _formats.UpdateFormat(updatedFormat);
        }

        #endregion

    }
}
