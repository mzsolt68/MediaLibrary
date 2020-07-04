using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Entities.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.MediaApi.Controllers.Common
{
    [Route("api/common/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ICommonService _service;

        public GenreController(ICommonService service)
        {
            _service = service;
        }

        [HttpGet("getgenres")]
        public async Task<ActionResult<ICollection<Genre>>> GetGenres()
        {
            var result = await _service.GetGenres();
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("getaudiogenres")]
        public async Task<ActionResult<ICollection<Genre>>> GetAudioGenres()
        {
            var result = await _service.GetAudioGenres();
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("getvideogenres")]
        public async Task<ActionResult<ICollection<Genre>>> GetVideoGenres()
        {
            var result = await _service.GetVideoGenres();
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("addgenre")]
        public async Task<ActionResult<Genre>> AddGenre([FromBody] Genre newGenre)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.AddGenre(newGenre);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("deletegenre/{id}")]
        public async Task<ActionResult> DeleteGenre(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }
            var result = await _service.DeleteGenre(id);
            if (result == 0)
            {
                return NotFound();
            }
            else if(result == -1)
            {
                return Conflict("Nem törölhető, mert zeneszám kapcsolódik hozzá!");
            }
            return Ok();
        }

        [HttpPut("updategenre")]
        public async Task<ActionResult<Genre>> UpdateGenre([FromBody] Genre updatedGenre)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.UpdateGenre(updatedGenre);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getgenrebyid/{id}")]
        public async Task<ActionResult<Genre>> GetGenreById(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }
            var result = await _service.GetGenreById(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
