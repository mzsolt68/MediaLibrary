using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLibrary.Common.Dto.Audio;
using MediaLibrary.Common.Interfaces.Services;
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

        [HttpPost("addtrack/{id}/{disc}")]
        public async Task<ActionResult<AudioTrackDto>> AddTrackToAlbum([FromBody]AudioTrackDto track, int? id, int? disc)
        {
            if(id == null || disc == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.AddTrackToAlbum(id, disc, track);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("deletetrack/{id}/{disc}/{track}")]
        public async Task<ActionResult> DeleteTrack(int? id, int? disc, int? track)
        {
            if(id == null || disc == null || track == null)
            {
                return BadRequest();
            }
            if(await _service.DeleteTrack(id, disc, track) == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("updatetrack/{id}/{disc}")]
        public async Task<ActionResult<AudioTrackDto>> UpdateTrack([FromBody] AudioTrackDto track, int? id, int? disc)
        {
            if(id == null || track == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.UpdateTrack(id, disc, track);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("updatetracklist/{id}/{disc}")]
        public async Task<ActionResult<ICollection<AudioTrackDto>>> UpdateTrackList([FromBody]ICollection<AudioTrackDto> tracklist, int? id, int? disc)
        {
            if(id == null || disc == null || tracklist == null)
            {
                return BadRequest();
            }
            var result = await _service.UpdateTrackList(id, disc, tracklist);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}