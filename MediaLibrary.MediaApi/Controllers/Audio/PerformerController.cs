using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MediaLibrary.MediaApi.Controllers.Audio
{
    [Route("api/audio/[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        private readonly IAudioService _service;

        public PerformerController(IAudioService service)
        {
            _service = service;
        }

        [HttpGet("getperformerlist")]
        public async Task<ActionResult<ICollection<SongPerformerDto>>> GetPerformerList()
        {
            var result = await _service.GetPerformers();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getperformer/{id}")]
        public async Task<ActionResult<PerformerDetailsDto>> GetPerformer(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var performer = await _service.GetPerformerById(id);
            if (performer == null)
            {
                return NotFound();
            }
            return Ok(performer);
        }

        [HttpGet("getperformercount")]
        public async Task<ActionResult<int>> GetPerformerCount()
        {
            return Ok(await _service.GetPerformerCount());
        }

        [HttpPost("addperformer")]
        public async Task<ActionResult<SongPerformerDto>> AddPerformer([FromBody]SongPerformerDto performer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.AddPerformer(performer);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("updateperformer")]
        public async Task<ActionResult<SongPerformerDto>> UpdatePerformer([FromBody]SongPerformerDto performer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.UpdatePerformer(performer);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("deleteperformer/{id}")]
        public async Task<ActionResult<int>> DeletePerformer(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            int result = await _service.DeletePerformer(id);
            if (result == 0)
            {
                return NotFound();
            }
            else if(result == -1)
            {
                return Conflict("Nem törölhető, mert egy vagy több zeneszám van hozzárendelve!");
            }
            return Ok();
        }
    }
}
