using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using SharedKernel;
using Application.Dto;
using Application.Dto.Common;

namespace Api.Controllers.Common
{
    [ApiController]
    [Route("api/common/languages")]
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

        [HttpPost("list")]
        public async Task<IActionResult> GetLanguages([FromBody] SearchParamsDTO searchParams)
        {
            if (searchParams == null)
            {
                return BadRequest();
            }
            var query = new GetLanguagesQuery(searchParams);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage([FromBody] LanguageDTO language)
        {
            var command = new CreateLanguageCommand(language.LanguageName);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetLanguageById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLanguage([FromBody] LanguageDTO language)
        {
            var command = new UpdateLanguageCommand(language.LanguageID, language.LanguageName);
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
