using DevFreela.Application.Commands.UserCommands.Create;
using DevFreela.Application.Queries.UserQueries.GetById;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Commands.UserCommands.CreateSkill;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPost("/skills")]
        public async Task<IActionResult> PostSkill( CreateUserSkillCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }
    }
}
