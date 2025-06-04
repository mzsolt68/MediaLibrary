using Application.Books;
using Application.Dto;
using Application.Dto.Books;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Books
{
    [Route("api/books/bookformats")]
    [ApiController]
    public class BookFormatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookFormatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookFormatById(Guid id)
        {
            var query = new GetBookFormatByIdQuery(id);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id:guid}/books")]
        public async Task<IActionResult> GetBooksOfFormat(Guid id)
        {
            var query = new GetBooksOfFormatQuery(id);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetBookFormats([FromBody]SearchParamsDTO searchParams)
        {
            if(searchParams is null)
            {
                return BadRequest();
            }
            var query = new GetBookFormatsQuery(searchParams);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookFormat([FromBody] BookFormatDTO bookFormat)
        {
            var command = new CreateBookFormatCommand(bookFormat.FormatName);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetBookFormatById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookFormat([FromBody] BookFormatDTO bookFormat)
        {
            var command = new UpdateBookFormatCommand(bookFormat.FormatID, bookFormat.FormatName);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBookFormat(Guid id)
        {
            var command = new DeleteBookFormatCommand(id);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
