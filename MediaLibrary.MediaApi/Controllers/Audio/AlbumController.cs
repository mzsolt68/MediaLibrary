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

        [HttpPut("updatealbum")]
        public async Task<ActionResult<AlbumDto>> UpdateAlbum([FromBody] AlbumDto album)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.UpdateAlbum(album);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}