using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;
using MediaLibrary.Repositories.Audio;

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

        public Album GetAlbumById(int id)
        {
            return _albums.GetAlbumById(id);
        }

        public List<Album> GetAlbums()
        {
            return _albums.GetAlbums();
        }

        public List<Album> GetAlbumsOfSong(Song song)
        {
            throw new NotImplementedException();
        }

        public AudioFormat GetFormatById(int id)
        {
            return _formats.GetFormatById(id);
        }

        public List<AudioFormat> GetFormats()
        {
            return _formats.GetFormats();
        }

        public Performer GetPerformerById(int id)
        {
            return _performers.GetPerformerById(id);
        }

        public List<Performer> GetPerformers()
        {
            return _performers.GetPerformers();
        }

        public List<Performer> GetPerformersOfSong(Song song)
        {
            throw new NotImplementedException();
        }

        public Song GetSongById(int id)
        {
            return _songs.GetSongById(id);
        }

        public List<Song> GetSongs()
        {
            throw new NotImplementedException();
        }

        public List<Song> GetSongsOfAlbum(Album album)
        {
            throw new NotImplementedException();
        }

        public List<Song> SongsOfPerformer(Performer performer)
        {
            throw new NotImplementedException();
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
