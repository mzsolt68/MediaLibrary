using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using SharedKernel;
using Application.Dto;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGenreById(Guid id)
        {
            var query = new GetGenreByIdQuery() { GenreId = id };
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetGenres([FromBody] SearchParamsDTO searchParams)
        {
            if(searchParams is null)
            {
                return BadRequest();
            }
            var query = new GetGenresQuery() { SearchParams = searchParams };
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetGenreById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] UpdateGenreCommand command)
        {
            if (id != command.GenreId) return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            var command = new DeleteGenreCommand(id); // Assuming this command exists
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
