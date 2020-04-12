using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.MediaApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.MediaApi.Repositories.Audio
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAlbum(Album newAlbum)
        {
            _context.Albums.Add(newAlbum);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAlbum(int? id)
        {
            var deleted = await _context.Albums.Where(a => a.AlbumID == id).SingleOrDefaultAsync();
            _context.Albums.Remove(deleted);
            int result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Album> GetAlbumById(int? id)
        {
            return await _context.Albums.Include(a => a.AlbumFormat).Where(a => a.AlbumID == id).AsNoTracking().SingleOrDefaultAsync();
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

        public void UpdateAlbum(Album updatedAlbum)
        {
            _context.Albums.Update(updatedAlbum);
            _context.SaveChanges();
        }
    }
}
