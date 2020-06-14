using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.MediaApi.Controllers.Audio
{
    [Route("api/audio/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAudioService _service;

        public AlbumController(IAudioService service)
        {
            _service = service;
        }

        [HttpGet("getalbumlist")]
        public async Task<ActionResult<ICollection<AlbumDto>>> GetAlbumList()
        {
            return Ok(await _service.GetAlbums());
        }

        [HttpGet("getalbumcount")]
        public async Task<ActionResult<int>> GetAlbumCount()
        {
            return Ok(await _service.GetAlbumCount());
        }

        [HttpGet("getalbumdetails/{id}")]
        public async Task<ActionResult<AlbumDetailsDto>> GetAlbumDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = await _service.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            return album;
        }

        [HttpDelete("deletealbum/{id}")]
        public async Task<ActionResult> DeleteAlbum(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            if(await _service.DeleteAlbum(id) == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("addalbum")]
        public async Task<ActionResult<AlbumDto>> AddAlbum([FromBody]AlbumDto album)
        {
            var result = await _service.AddAlbum(album);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
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


    }
}