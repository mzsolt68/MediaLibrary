using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Entities.Dto.Audio;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.MediaApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.MediaApi.Controllers.Audio
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService service;
        private AlbumDetailsDto albumDetails;

        public AudioController(IAudioService _service)
        {
            service = _service;
        }

        [HttpGet("getalbumlist")]
        public async Task<ActionResult<ICollection<Album>>> GetAlbumList()
        {
            return Ok(await service.GetAlbums());
        }

        [HttpGet("getalbumcount")]
        public async Task<ActionResult<int>> GetAlbumCount()
        {
            return Ok(await service.GetAlbumCount());
        }

        [HttpGet("getalbumdetails/{id}")]
        public async Task<ActionResult<AlbumDetailsDto>> GetAlbumDetails(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var album = await service.GetAlbumById(id);
            if (album == null)
            {
                return null;
            }
            if (albumDetails != null)
            {
                albumDetails = null;
            }
            albumDetails = await CreateDetailsViewModel(album);
            return albumDetails;
        }

        // GET: Albums/Create
        //public IActionResult Create()
        //{
        //    Album album = new Album();
        //    if (_editViewModel != null)
        //    {
        //        _editViewModel = null;
        //    }
        //    _editViewModel = CreateEditViewModel(album);
        //    return View(_editViewModel);
        //}

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(int AudioFormatID, [Bind("AlbumID,AlbumTitle,NrOfDiscs")] Album album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        album.AlbumFormat = _service.GetFormatById(AudioFormatID);
        //        _service.AddAlbum(album);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(album);
        //}

        // GET: Albums/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var album = _service.GetAlbumById(id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }
        //    if (_editViewModel != null)
        //    {
        //        _editViewModel = null;
        //    }
        //    _editViewModel = CreateEditViewModel(album);
        //    return View(_editViewModel);
        //}

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, int AudioFormatID, [Bind("AlbumID,AlbumTitle,NrOfDiscs")] Album album)
        //{
        //    if (id != album.AlbumID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            album.AlbumFormat = _service.GetFormatById(AudioFormatID);
        //            _service.UpdateAlbum(album);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AlbumExists(album.AlbumID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(album);
        //}

        // GET: Albums/Delete/5
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var album = _service.GetAlbumById(id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }
        //    if (_detailsViewModel != null)
        //    {
        //        _detailsViewModel = null;
        //    }
        //    _detailsViewModel = CreateDetailsViewModel(album);
        //    return View(_detailsViewModel);
        //}

        // POST: Albums/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var album = _service.GetAlbumById(id);
        //    _service.DeleteAlbum(album);
        //    return RedirectToAction(nameof(Index));
        //}

        private async Task<bool> AlbumExists(int id)
        {
            return await service.GetAlbumById(id) != null;
        }

        //private AlbumEditViewModel CreateEditViewModel(Album album)
        //{
        //    var vm = new AlbumEditViewModel();
        //    if (album != null)
        //    {
        //        vm.Album = album;
        //        vm.AudioFormats = _service.GetFormatsToViews();
        //        if (album.AlbumFormat != null)
        //        {
        //            vm.AudioFormatID = album.AlbumFormat.AudioFormatID;
        //        }
        //        var songs = _service.GetSongsOfAlbum(album);
        //        foreach (var item in songs)
        //        {
        //            vm.Songs.Add(new AlbumSongViewModel { SongOfAlbum = item });
        //        }
        //    }
        //    return vm;
        //}

        private async Task<AlbumDetailsDto> CreateDetailsViewModel(Album album)
        {
            var dto = new AlbumDetailsDto();
            if (album != null)
            {
                dto.Album = album;
                dto.Details = await service.GetSongsOfAlbum(album);
            }
            return dto;
        }

        [HttpGet("getsonglist")]
        public async Task<ActionResult<ICollection<Song>>> GetSongList()
        {
            return Ok(await service.GetSongs());
        }

        [HttpGet("getsong/{id}")]
        public async Task<ActionResult<Song>> GetSong(int? id)
        {
            if(id == null)
            {
                return null;
            }
            var song = await service.GetSongById(id);
            if(song == null)
            {
                return null;
            }
            return song;
        }

    }
}