using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;
using MediaLibrary.Repositories.Audio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Services.Audio
{
    public class AudioService : IAudioService
    {
        private ApplicationDbContext _context;
        private IAlbumRepository _albums;
        private IAudioFormatRepository _formats;
        private IPerformerReopsitory _performers;
        private ISongRepository _songs;

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

        public void AddSong(Song newSong)
        {
            _songs.AddSong(newSong);
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

        public Album GetAlbumById(int? id)
        {
            return _albums.GetAlbumById(id);
        }

        public ICollection<Album> GetAlbums()
        {
            return _albums.GetAlbums();
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

        public Song GetSongById(int? id)
        {
            return _songs.GetSongById(id);
        }

        public ICollection<Song> GetSongs()
        {
            return _songs.GetSongs();
        }

        public ICollection<AlbumSong> GetSongsOfAlbum(Album album)
        {
            return _albums.GetSongsOfAlbum(album);
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

        public void UpdateSong(Song updatedSong)
        {
            _songs.UpdateSong(updatedSong);
        }
    }
}
