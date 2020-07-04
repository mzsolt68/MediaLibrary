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
            int result = await _service.DeleteSong(id);
            if (result == 0)
            {
                return NotFound();
            }
            else if(result == -1)
            {
                return Conflict("Nem törölhető, mert egy vagy több albumhoz van rendelve!");
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

        [HttpGet("getperformersofsong/{id}")]
        public async Task<ActionResult<ICollection<SongPerformerDto>>> GetPerformersOfSong(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }
            var result = await _service.GetPerformersOfSong(id);
            if(result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
