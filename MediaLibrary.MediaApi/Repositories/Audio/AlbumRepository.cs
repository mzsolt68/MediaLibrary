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
        private ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAlbum(Album newAlbum)
        {
            _context.Albums.Add(newAlbum);
            _context.SaveChanges();
        }

        public void DeleteAlbum(Album deletedAlbum)
        {
            _context.Albums.Remove(deletedAlbum);
            _context.SaveChanges();
        }

        public async Task<Album> GetAlbumById(int? id)
        {
            return await _context.Albums.Include(a => a.AlbumFormat).Where(a => a.AlbumID == id).SingleOrDefaultAsync();
        }

        public async Task<int> GetAlbumCount()
        {
            return await _context.Albums.CountAsync();
        }

        public async Task<ICollection<Album>> GetAlbums()
        {
            var albumList = await _context.Albums.OrderBy(a => a.AlbumTitle).ToListAsync();
            foreach (var item in albumList)
            {
                item.NrOfSongs = await _context.AlbumSongs.Where(a => a.AlbumID == item.AlbumID).CountAsync();
            }
            return albumList;
        }

        public async Task<ICollection<AlbumSong>> GetSongsOfAlbum(Album album)
        {
            var aslist = await _context.AlbumSongs.Include(x => x.Song).ThenInclude(songPerformer => songPerformer.PerformerSongs).OrderBy(a => a.TrackNr).Where(als => als.Album == album).ToListAsync();
            foreach (var item in aslist)
            {
                item.Song.PerformerSongs = await _context.PerformerSongs.Include(x => x.Performer).Where(ps => ps.Song == item.Song).ToListAsync();
            }
            return aslist;
        }

        public void UpdateAlbum(Album updatedAlbum)
        {
            _context.Albums.Update(updatedAlbum);
            _context.SaveChanges();
        }
    }
}
