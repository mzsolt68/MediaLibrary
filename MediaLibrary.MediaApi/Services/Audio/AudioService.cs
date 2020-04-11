using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.MediaApi.Interfaces;
using MediaLibrary.MediaApi.Repositories.Audio;
using MediaLibrary.Entities.Dto.Audio;
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

        public void AddAlbum(Album newAlbum)
        {
            _albums.AddAlbum(newAlbum);
        }

        public void AddFormat(AudioFormat newFormat)
        {
            _formats.AddFormat(newFormat);
        }

        public void AddPerformer(Performer newPerformer)
        {
            _performers.AddPerformer(newPerformer);
        }

        public void AddSong(Song newSong, List<SongPerformerDto> performers)
        {
            _songs.AddSong(newSong, performers);
        }

        public void DeleteAlbum(Album deletedAlbum)
        {
            _albums.DeleteAlbum(deletedAlbum);
        }

        public void DeleteFormat(AudioFormat deletedFormat)
        {
            _formats.DeleteFormat(deletedFormat);
        }

        public void DeletePerformer(Performer deletedPerformer)
        {
            _performers.DeletePerformer(deletedPerformer);
        }

        public void DeleteSong(Song deletedSong)
        {
            _songs.DeleteSong(deletedSong);
        }

        public async Task<AlbumDetailsDto> GetAlbumById(int? id)
        {
            AlbumDetailsDto result = null;
            var album = await _albums.GetAlbumById(id);
            if (album != null)
            {
                result = new AlbumDetailsDto
                {
                    Album = new AlbumDto()
                };
                result.Album.AlbumID = album.AlbumID;
                result.Album.Title = album.AlbumTitle;
                result.Album.Nr_of_discs = album.NrOfDiscs;
                result.Album.Format = album.AlbumFormat.AudioFormatName;
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
                    result.Album.Nr_of_tracks += d.Tracks.Count;
                }
            }
            return result;
        }

        public async Task<ICollection<AlbumDto>> GetAlbums()
        {
            List<AlbumDto> result = null;
            var albums = await _albums.GetAlbums();
            if(albums.Count > 0)
            {
                result = new List<AlbumDto>();
                foreach(var album in albums)
                {
                    AlbumDto a = new AlbumDto
                    {
                        AlbumID = album.AlbumID,
                        Title = album.AlbumTitle,
                        Format = album.AlbumFormat.AudioFormatName,
                        Nr_of_discs = album.NrOfDiscs,
                        Nr_of_tracks = album.NrOfSongs
                    };
                    result.Add(a);
                }
            }
            return result;
        }

        public ICollection<Album> GetAlbumsOfSong(Song song)
        {
            return _songs.GetAlbumsOfSong(song);
        }

        public AudioFormat GetFormatById(int? id)
        {
            return _formats.GetFormatById(id);
        }

        public ICollection<AudioFormat> GetFormats()
        {
            return _formats.GetFormats();
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

        public Performer GetPerformerById(int? id)
        {
            return _performers.GetPerformerById(id);
        }

        public ICollection<Performer> GetPerformers()
        {
            return _performers.GetPerformers();
        }

        public ICollection<Performer> GetPerformersOfSong(Song song)
        {
            return _songs.GetPerformersOfSong(song);
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

        public async Task<SongDetailsDto> GetSongById(int? id)
        {
            SongDetailsDto result = null;
            var song = await _songs.GetSongById(id);
            if(song != null)
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
                            Title = album.Album.AlbumTitle
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
            if(songs.Count > 0)
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

        public void UpdateAlbum(Album updatedAlbum)
        {
            _albums.UpdateAlbum(updatedAlbum);
        }

        public void UpdateFormat(AudioFormat updatedFormat)
        {
            _formats.UpdateFormat(updatedFormat);
        }

        public void UpdatePerformer(Performer updatedPerformer)
        {
            _performers.UpdatePerformer(updatedPerformer);
        }

        public void UpdateSong(Song updatedSong, List<SongPerformerDto> performers)
        {
            _songs.UpdateSong(updatedSong, performers);
        }

        public async Task<int> GetAlbumCount()
        {
            return await _albums.GetAlbumCount();
        }

        public int GetPerformerCount()
        {
            return _performers.GetPerformerCount();
        }

        public int GetSongCount()
        {
            return _songs.GetSongCount();
        }
    }
}
