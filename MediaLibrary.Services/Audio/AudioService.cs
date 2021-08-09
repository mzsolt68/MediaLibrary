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
            var result = await _albums.AddAlbum(newAlbum.AsAlbum());
            if (result != null)
            {
                return result.AsAlbumDto();
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
                var songlist = (await _albums.GetSongsOfAlbum(album)).GroupBy(d => d.Disc);
                result = new AlbumDetailsDto
                {
                    Album = album.AsAlbumDto(),
                    Discs = new List<AudioDiscDto>()
                };
                foreach (var disc in songlist)
                {
                    result.Discs.Add(disc.AsAudioDiscDto());
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
                result = albums.Select(a => a.AsAlbumDto()).ToList();
            }
            return result;
        }

        public async Task<AlbumDto> UpdateAlbum(AlbumDto updatedAlbum)
        {
            var result = await _albums.UpdateAlbum(updatedAlbum.AsAlbum());
            return result?.AsAlbumDto();
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
            return result?.AsSongPerformerDto();
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
                    Performer = performer.AsSongPerformerDto()
                };
                if (performer.PerformerSongs.Count > 0)
                {
                    result.Songs = performer.PerformerSongs.Select(ps => ps.Song.AsSongDto(false)).ToList();
                }
            }
            return result;
        }

        public async Task<ICollection<SongPerformerDto>> GetPerformers()
        {
            var performers = await _performers.GetPerformers();
            return performers.Count > 0 ? performers.Select(p => p.AsSongPerformerDto()).ToList() : null;
        }

        public async Task<SongPerformerDto> UpdatePerformer(SongPerformerDto updatedPerformer)
        {
            var result = await _performers.UpdatePerformer(updatedPerformer.AsPerformer());
            if (result != null)
            {
                return result.AsSongPerformerDto();
            }
            return null;
        }

        public async Task<int> GetPerformerCount()
        {
            return await _performers.GetPerformerCount();
        }

        public async Task<ICollection<SongDto>> GetSongsOfPerformer(int? performerId)
        {
            if (!performerId.HasValue)
            {
                return null;
            }
            var songs = await _performers.GetSongsOfPerformer(performerId);
            return songs?.Select(s => s.AsSongDto(false)).ToList();
        }

        #endregion

        #region Song

        public async Task<SongDto> AddSong(SongDto newSong)
        {
            ICollection<int> performers;
            var result = await _songs.AddSong(newSong.AsSong(out performers), performers);
            if (result != null)
            {
                return result.AsSongDto();
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
                result = song.AsSongDetailsDto();
            }
            return result;
        }

        public async Task<ICollection<SongDto>> GetSongs()
        {
            ICollection<SongDto> result = null;
            var songs = await _songs.GetSongs();
            if (songs.Count > 0)
            {
                result = songs.Select(s => s.AsSongDto()).ToList();
            }
            return result;
        }

        public async Task<SongDto> UpdateSong(SongDto updatedSong)
        {
            ICollection<int> performers;
            var result = await _songs.UpdateSong(updatedSong.AsSong(out performers), performers);
            if (result != null)
            {
                return result.AsSongDto();
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
            return performers.Select(p => p.AsSongPerformerDto()).ToList();
        }

        public async Task<ICollection<AlbumDto>> GetAlbumsOfSong(int? songId)
        {
            if (!songId.HasValue)
            {
                return null;
            }
            var albums = await _songs.GetAlbumsOfSong(songId);
            if (albums == null)
            {
                return null;
            }
            var albumsOfSong = new List<AlbumDto>();
            foreach (var album in albums)
            {
                album.NrOfSongs = await _albums.GetSongCountOfAlbum(album.AlbumID);
                albumsOfSong.Add(album.AsAlbumDto());
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

        #region Track

        public async Task<AudioTrackDto> AddTrackToAlbum(int? albumID, int? discNr, AudioTrackDto track)
        {
            var result = await _albums.AddTrack(track.AsAlbumSong(albumID, discNr));
            if (result != null)
            {
                return result.AsAudioTrackDto();
            }
            return null;
        }

        public async Task<int> DeleteTrack(int? albumID, int? discNr, int? trackNr)
        {
            return await _albums.DeleteTrack(albumID, discNr, trackNr);
        }

        public async Task<AudioTrackDto> UpdateTrack(int? albumID, int? discNr, AudioTrackDto updatedTrack)
        {
            var result = await _albums.UpdateTrack(updatedTrack.AsAlbumSong(albumID, discNr));
            if (result != null)
            {
                return result.AsAudioTrackDto();
            }
            return null;
        }

        public async Task<IEnumerable<AudioTrackDto>> UpdateTrackList(int? albumID, int? discNr, IEnumerable<AudioTrackDto> trackList)
        {
            if (trackList != null && trackList.Count() > 0)
            {
                var albumSongList = trackList.Select(t => t.AsAlbumSong(albumID, discNr));
                var result = await _albums.UpdateTrackList(albumSongList);
                if (result != null && result.Count() > 0)
                {
                    return result.Select(s => s.AsAudioTrackDto());
                }
            }
            return null;
        }

        #endregion
    }
}
