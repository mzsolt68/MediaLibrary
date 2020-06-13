using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Common.Interfaces.Audio;
using MediaLibrary.MediaApi.Repositories.Audio;
using MediaLibrary.Common.Dto.Audio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.MediaApi.Services.Audio
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
            var addedAlbum = new Album
            {
                AlbumTitle = newAlbum.Title,
                AudioFormatID = newAlbum.Format.AudioFormatID,
                NrOfDiscs = (byte) newAlbum.Nr_of_discs
            };
            var result = await _albums.AddAlbum(addedAlbum);
            result.AlbumFormat = await _formats.GetFormatById(result.AudioFormatID);
            if(result != null)
            {
                return ConvertAlbumToDto(result);
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
                    Album = ConvertAlbumToDto(album)
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
                    AlbumDto a = ConvertAlbumToDto(album);
                    result.Add(a);
                }
            }
            return result;
        }

        public ICollection<Album> GetAlbumsOfSong(Song song)
        {
            return _songs.GetAlbumsOfSong(song);
        }

        public void UpdateAlbum(Album updatedAlbum)
        {
            _albums.UpdateAlbum(updatedAlbum);
        }

        public async Task<int> GetAlbumCount()
        {
            return await _albums.GetAlbumCount();
        }

        private AlbumDto ConvertAlbumToDto(Album album)
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
        #endregion

        #region Performer
        public void AddPerformer(Performer newPerformer)
        {
            _performers.AddPerformer(newPerformer);
        }

        public void DeletePerformer(Performer deletedPerformer)
        {
            _performers.DeletePerformer(deletedPerformer);
        }

        public async Task<PerformerDetailsDto> GetPerformerById(int? id)
        {
            PerformerDetailsDto result = null;
            var performer = await _performers.GetPerformerById(id);
            if (performer != null)
            {
                result = new PerformerDetailsDto
                {
                    Performer = new PerformerDto
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

        public async Task<ICollection<PerformerDto>> GetPerformers()
        {
            List<PerformerDto> result = null;
            var performers = await _performers.GetPerformers();
            if (performers.Count > 0)
            {
                result = new List<PerformerDto>();
                foreach (var performer in performers)
                {
                    PerformerDto p = new PerformerDto
                    {
                        PerformerID = performer.PerformerID,
                        Name = performer.PerformerName
                    };
                    result.Add(p);
                }
            }
            return result;
        }

        public ICollection<Performer> GetPerformersOfSong(Song song)
        {
            return _songs.GetPerformersOfSong(song);
        }

        public void UpdatePerformer(Performer updatedPerformer)
        {
            _performers.UpdatePerformer(updatedPerformer);
        }

        public async Task<int> GetPerformerCount()
        {
            return await _performers.GetPerformerCount();
        }

        public IEnumerable<SelectListItem> GetPerformersToViews()
        {
            List<SelectListItem> performers = _context.Performers.AsNoTracking()
                .OrderBy(perf => perf.PerformerName)
                .Select(p =>
                new SelectListItem
                {
                    Value = p.PerformerID.ToString(),
                    Text = p.PerformerName
                }).ToList();
            performers.Insert(0, new SelectListItem { Value = null, Text = "--- Válassz előadót ---" });
            return new SelectList(performers, "Value", "Text");
        }
        #endregion

        #region Song
        public void AddSong(Song newSong, List<SongPerformerDto> performers)
        {
            _songs.AddSong(newSong, performers);
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
                result.Song.Performers = new List<PerformerDto>();
                foreach (var perf in song.PerformerSongs)
                {
                    PerformerDto p = new PerformerDto
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
                    result.Albums = new List<AlbumDto>();
                    foreach (var album in song.AlbumSongs)
                    {
                        AlbumDto a = new AlbumDto
                        {
                            AlbumID = album.Album.AlbumID,
                            Title = album.Album.AlbumTitle,
                            Nr_of_discs = album.Album.NrOfDiscs,
                            Nr_of_tracks = await _albums.GetSongsOfAlbum(album.Album.AlbumID)
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
                        Performers = new List<PerformerDto>()
                    };
                    foreach (var perf in song.PerformerSongs)
                    {
                        PerformerDto p = new PerformerDto
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

        public async Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album)
        {
            return await _albums.GetSongsOfAlbum(album);
        }

        public ICollection<PerformerSong> SongsOfPerformer(Performer performer)
        {
            return _performers.SongsOfPerformer(performer);
        }

        public void UpdateSong(Song updatedSong, List<SongPerformerDto> performers)
        {
            _songs.UpdateSong(updatedSong, performers);
        }

        public async Task<int> GetSongCount()
        {
            return await _songs.GetSongCount();
        }
        #endregion

        #region Format
        public void AddFormat(AudioFormat newFormat)
        {
            _formats.AddFormat(newFormat);
        }

        public void DeleteFormat(AudioFormat deletedFormat)
        {
            _formats.DeleteFormat(deletedFormat);
        }

        public async Task<AudioFormat> GetFormatById(int? id)
        {
            return await _formats.GetFormatById(id);
        }

        public async Task<ICollection<AudioFormat>> GetFormats()
        {
            return await _formats.GetFormats();
        }

        public IEnumerable<SelectListItem> GetFormatsToViews()
        {
            List<SelectListItem> formats = _context.AudioFormats.AsNoTracking()
                .OrderBy(af => af.AudioFormatName)
                .Select(f =>
                new SelectListItem
                {
                    Value = f.AudioFormatID.ToString(),
                    Text = f.AudioFormatName
                }).ToList();
            formats.Insert(0, new SelectListItem { Value = null, Text = "--- Válassz formátumot ---" });
            return new SelectList(formats, "Value", "Text");
        }

        public void UpdateFormat(AudioFormat updatedFormat)
        {
            _formats.UpdateFormat(updatedFormat);
        }
        #endregion
    }
}
