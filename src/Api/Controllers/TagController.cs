using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using SharedKernel;
using Application.Dto;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            var query = new GetTagByIdQuery(id);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetTags([FromBody] SearchParamsDTO searchParams)
        {
            if(searchParams is null)
            {
                return BadRequest();
            }
            var query = new GetTagsQuery(searchParams);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetTagById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] UpdateTagCommand command)
        {
            if (id != command.TagId) return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var command = new DeleteTagCommand(id);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
