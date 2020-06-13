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
    [Route("api/[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        private readonly IAudioService _service;

        public PerformerController(IAudioService service)
        {
            _service = service;
        }

        [HttpGet("getperformerlist")]
        public async Task<ActionResult<ICollection<PerformerDto>>> GetPerformerList()
        {
            return Ok(await _service.GetPerformers());
        }

        [HttpGet("getperformer/{id}")]
        public async Task<ActionResult<PerformerDetailsDto>> GetPerformer(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var performer = await _service.GetPerformerById(id);
            if (performer == null)
            {
                return null;
            }
            return performer;
        }

        [HttpGet("getperformercount")]
        public async Task<ActionResult<int>> GetPerformerCount()
        {
            return Ok(await _service.GetPerformerCount());
        }

    }
}
