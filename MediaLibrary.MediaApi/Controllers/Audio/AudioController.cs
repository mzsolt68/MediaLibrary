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

        public AudioController(IAudioService _service)
        {
            service = _service;
        }

        #region Album
        [HttpGet("getalbumlist")]
        public async Task<ActionResult<ICollection<AlbumDto>>> GetAlbumList()
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
            return album;
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
        #endregion

        #region Song
        [HttpGet("getsonglist")]
        public async Task<ActionResult<ICollection<SongDto>>> GetSongList()
        {
            return Ok(await service.GetSongs());
        }

        [HttpGet("getsong/{id}")]
        public async Task<ActionResult<SongDetailsDto>> GetSong(int? id)
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

        [HttpGet("getsongcount")]
        public async Task<ActionResult<int>> GetSongCount()
        {
            return Ok(await service.GetSongCount());
        }
        #endregion

        #region Performer
        [HttpGet("getperformerlist")]
        public async Task<ActionResult<ICollection<PerformerDto>>> GetPerformerList()
        {
            return Ok(await service.GetPerformers());
        }

        [HttpGet("getperformer/{id}")]
        public async Task<ActionResult<PerformerDetailsDto>> GetPerformer(int? id)
        {
            if(id == null)
            {
                return null;
            }
            var performer = await service.GetPerformerById(id);
            if(performer == null)
            {
                return null;
            }
            return performer;
        }

        [HttpGet("getperformercount")]
        public async Task<ActionResult<int>> GetPerformerCount()
        {
            return Ok(await service.GetPerformerCount());
        }
        #endregion
    }
}