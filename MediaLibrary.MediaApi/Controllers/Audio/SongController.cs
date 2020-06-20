using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.MediaApi.Controllers.Audio
{
    [Route("api/audio/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IAudioService _service;

        public SongController(IAudioService service)
        {
            _service = service;
        }

        [HttpGet("getsonglist")]
        public async Task<ActionResult<ICollection<SongDto>>> GetSongList()
        {
            return Ok(await _service.GetSongs());
        }

        [HttpGet("getsong/{id}")]
        public async Task<ActionResult<SongDetailsDto>> GetSong(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var song = await _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            return song;
        }

        [HttpGet("getsongcount")]
        public async Task<ActionResult<int>> GetSongCount()
        {
            return Ok(await _service.GetSongCount());
        }

        [HttpDelete("deletesong/{id}")]
        public async Task<ActionResult> DeleteSong(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (await _service.DeleteSong(id) == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("addsong")]
        public async Task<ActionResult<SongDto>> AddSong([FromBody]SongDto newSong)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.AddSong(newSong);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("updatesong")]
        public async Task<ActionResult<SongDto>> UpdateSong([FromBody]SongDto updatedSong)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.UpdateSong(updatedSong);
            if(result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}
