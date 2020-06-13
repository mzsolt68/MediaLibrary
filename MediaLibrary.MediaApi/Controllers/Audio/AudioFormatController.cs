using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Entities.Models.Audio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.MediaApi.Controllers.Audio
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioFormatController : ControllerBase
    {
        private readonly IAudioService _service;

        public AudioFormatController(IAudioService service)
        {
            _service = service;
        }

        [HttpGet("getformatlist")]
        public async Task<ActionResult<ICollection<AudioFormat>>> GetAudioFormatList()
        {
            return Ok(await _service.GetFormats());
        }

        [HttpGet("getformat/{id}")]
        public async Task<ActionResult<AudioFormat>> GetAudioFormat(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var audioformat = await _service.GetFormatById(id);
            if (audioformat == null)
            {
                return null;
            }
            return audioformat;
        }

        [HttpPost("addformat")]
        public async Task<ActionResult<AudioFormat>> AddNewFormat([FromBody]AudioFormat newFormat)
        {
            var result = await _service.AddFormat(newFormat);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("updateformat")]
        public async Task<ActionResult<AudioFormat>> UpdateFormat([FromBody]AudioFormat format)
        {
            var result = await _service.UpdateFormat(format);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
