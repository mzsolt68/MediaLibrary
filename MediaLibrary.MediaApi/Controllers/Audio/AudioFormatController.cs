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

        [HttpGet("getaudioformatlist")]
        public async Task<ActionResult<ICollection<AudioFormat>>> GetAudioFormatList()
        {
            return Ok(await _service.GetFormats());
        }

        [HttpGet("getaudioformat/{id}")]
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

    }
}
