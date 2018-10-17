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
            throw new NotImplementedException();
        }

        public void AddFormat(AudioFormat newFormat)
        {
            throw new NotImplementedException();
        }

        public void AddPerformer(Performer newPerformer)
        {
            throw new NotImplementedException();
        }

        public void AddSong(Song newSong)
        {
            throw new NotImplementedException();
        }

        public void DeleteAlbum(Album deletedAlbum)
        {
            throw new NotImplementedException();
        }

        public void DeleteFormat(AudioFormat deletedFormat)
        {
            throw new NotImplementedException();
        }

        public void DeletePerformer(Performer deletedPerformer)
        {
            throw new NotImplementedException();
        }

        public void DeleteSong(Song deletedSong)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbumById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Album> GetAlbums()
        {
            throw new NotImplementedException();
        }

        public List<Album> GetAlbumsOfSong(Song song)
        {
            throw new NotImplementedException();
        }

        public AudioFormat GetFormatById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AudioFormat> GetFormats()
        {
            throw new NotImplementedException();
        }

        public Performer GetPerformerById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Performer> GetPerformers()
        {
            throw new NotImplementedException();
        }

        public List<Performer> GetPerformersOfSong(Song song)
        {
            throw new NotImplementedException();
        }

        public Song GetSongById(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateFormat(AudioFormat updatedFormat)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerformer(Performer updatedPerformer)
        {
            throw new NotImplementedException();
        }

        public void UpdateSong(Song updatedSong)
        {
            throw new NotImplementedException();
        }
    }
}
