using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Audio;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MediaLibrary.Repositories.Audio
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Album> AddAlbum(Album newAlbum)
        {
            if (!await _context.Albums.Where(a => a.AlbumTitle.ToLower() == newAlbum.AlbumTitle.ToLower() && a.AudioFormatID == newAlbum.AudioFormatID).AnyAsync())
            {
                _context.Albums.Add(newAlbum);
                await _context.SaveChangesAsync();
                newAlbum.AlbumFormat = await _context.AudioFormats.SingleOrDefaultAsync(f => f.AudioFormatID == newAlbum.AudioFormatID);
                return newAlbum;
            }
            return null;
        }

        public async Task<int> DeleteAlbum(int? id)
        {
            int result = 0;
            var deleted = await _context.Albums.Include(als => als.AlbumSongs).SingleOrDefaultAsync(a => a.AlbumID == id);
            if (deleted != null)
            {
                foreach (var item in deleted.AlbumSongs)
                {
                    _context.AlbumSongs.Remove(item);
                }
                _context.Albums.Remove(deleted);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Album> GetAlbumById(int? id)
        {
            var result = await _context.Albums.Include(a => a.AlbumFormat).Where(a => a.AlbumID == id).AsNoTracking().SingleOrDefaultAsync();
            result.NrOfSongs = await _context.AlbumSongs.Where(a => a.AlbumID == result.AlbumID).CountAsync();
            return result;
        }

        public async Task<int> GetAlbumCount()
        {
            return await _context.Albums.CountAsync();
        }

        public async Task<ICollection<Album>> GetAlbums()
        {
            var albumList = await _context.Albums.Include(a => a.AlbumFormat).OrderBy(a => a.AlbumTitle).AsNoTracking().ToListAsync();
            foreach (var item in albumList)
            {
                item.NrOfSongs = await _context.AlbumSongs.Where(a => a.AlbumID == item.AlbumID).CountAsync();
            }
            return albumList;
        }

        public async Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album)
        {
            var aslist = await _context.AlbumSongs.Include(x => x.Song).ThenInclude(songPerformer => songPerformer.PerformerSongs)
                .ThenInclude(pfs => pfs.Performer).OrderBy(a => a.TrackNr).Where(als => als.Album == album).AsNoTracking().ToListAsync();
            return aslist;
        }

        public async Task<int> GetSongsOfAlbum(int id)
        {
            return await _context.AlbumSongs.Where(a => a.AlbumID == id).AsNoTracking().CountAsync();
        }

        public async Task<Album> UpdateAlbum(Album updatedAlbum)
        {
            var dbAlbum = await _context.Albums.SingleOrDefaultAsync(a => a.AlbumID == updatedAlbum.AlbumID);
            if(dbAlbum != null)
            {
                dbAlbum.AlbumTitle = updatedAlbum.AlbumTitle;
                dbAlbum.AudioFormatID = updatedAlbum.AudioFormatID;
                dbAlbum.NrOfDiscs = updatedAlbum.NrOfDiscs;
                await _context.SaveChangesAsync();
                dbAlbum.AlbumFormat = await _context.AudioFormats.SingleOrDefaultAsync(f => f.AudioFormatID == dbAlbum.AudioFormatID);
            }
            return dbAlbum;
        }

        public async Task<AlbumSong> AddTrack(AlbumSong newTrack)
        {
            AlbumSong result = null;
            var dbAlbum = await _context.Albums
                .Include(a => a.AlbumSongs)
                .Where(a => a.AlbumID == newTrack.AlbumID)
                .AsNoTracking()
                .SingleOrDefaultAsync();
            if (dbAlbum != null)
            {
                if (trackCanBeAdded())
                {
                    _context.AlbumSongs.Add(newTrack);
                    if (await _context.SaveChangesAsync() == 1)
                    {
                        result = await _context.AlbumSongs.Include(x => x.Song).ThenInclude(sp => sp.PerformerSongs)
                            .ThenInclude(pfs => pfs.Performer)
                            .Where(als => als.AlbumID == newTrack.AlbumID && als.SongID == newTrack.SongID && als.TrackNr == newTrack.TrackNr)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();
                    }
                }
            }
            return result;

            bool trackCanBeAdded()
            {
                if(newTrack.Disc > dbAlbum.NrOfDiscs)
                {
                    return false;
                }
                if(dbAlbum.AlbumSongs.FirstOrDefault(als => als.Disc == newTrack.Disc && als.TrackNr == newTrack.TrackNr) != null)
                {
                    return false;
                }
                if(dbAlbum.AlbumSongs
                    .FirstOrDefault(als =>
                        als.SongID == newTrack.SongID &&
                        als.Note == newTrack.Note &&
                        als.PlayTime.Hour == newTrack.PlayTime.Hour && als.PlayTime.Minute == newTrack.PlayTime.Minute) != null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<int> DeleteTrack(int? albumID, int? discNr, int? trackNr)
        {
            int result = 0;
            var dbtrack = await _context.AlbumSongs
                .Where(als => als.AlbumID == albumID && als.Disc == discNr && als.TrackNr == trackNr)
                .FirstOrDefaultAsync();
            if(dbtrack != null)
            {
                _context.AlbumSongs.Remove(dbtrack);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<AlbumSong> UpdateTrack(AlbumSong updatedTrack)
        {
            var dbtrack = await _context.AlbumSongs.Where(x => x.AlbumID == updatedTrack.AlbumID && x.Disc == updatedTrack.Disc && x.TrackNr == updatedTrack.TrackNr).SingleOrDefaultAsync();
            if (dbtrack != null)
            {
                dbtrack.SongID = updatedTrack.SongID;
                dbtrack.PlayTime = updatedTrack.PlayTime;
                dbtrack.Note = updatedTrack.Note;
                await _context.SaveChangesAsync();
                return dbtrack;
            }
            return null;
        }

        public async Task<IEnumerable<AlbumSong>> UpdateTrackList(IEnumerable<AlbumSong> trackList)
        {
            var dbtrscklist = await _context.AlbumSongs.Where(x => x.AlbumID == trackList.First().AlbumID && x.Disc == trackList.First().Disc).ToListAsync();
            if (dbtrscklist != null && dbtrscklist.Count() > 0)
            {
                //TODO implementálni a lista frissítését
                await _context.SaveChangesAsync();
                return dbtrscklist;
            }
            return null;
        }
    }
}
