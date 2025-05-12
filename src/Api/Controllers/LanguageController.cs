using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using SharedKernel;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/languages")]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLanguageById(Guid id)
        {
            var query = new GetLanguageByIdQuery(id); // Assuming this query exists
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage([FromBody] CreateLanguageCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetLanguageById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLanguage(Guid id, [FromBody] UpdateLanguageCommand command)
        {
            if (id != command.LanguageId) return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLanguage(Guid id)
        {
            var command = new DeleteLanguageCommand(id); // Assuming this command exists
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
