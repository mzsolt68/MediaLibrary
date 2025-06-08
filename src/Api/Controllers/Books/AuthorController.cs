using Application.Books;
using Application.Common;
using Application.Dto;
using Application.Dto.Books;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Extensions;

namespace Api.Controllers.Books
{
    [Route("api/books/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id:guid}/books")]
        public async Task<IActionResult> GetBooksOfAuthor(Guid id)
        {
            var query = new GetBooksOfAuthorQuery(id);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetAuthors([FromBody]SearchParamsDTO searchParams)
        {
            if(searchParams is null)
            {
                return BadRequest();
            }
            var query = new GetAuthorsQuery(searchParams);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO author)
        {
            var command = new CreateAuthorCommand(author.FirstName, author.LastName, author.MiddleName);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetAuthorById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody]BookAuthorDTO author)
        {
            var command = new UpdateAuthorCommand(author.AuthorID, author.FirstName, author.LastName, author.MiddleName);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var command = new DeleteAuthorCommand(id);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

    }
}
