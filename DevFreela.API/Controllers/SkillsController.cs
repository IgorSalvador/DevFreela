using DevFreela.Application.Commands.SkillCommands.CreateSkill;
using DevFreela.Application.Queries.SkillQueries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "", int page = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetSkillsQuery(search, page, pageSize), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSkillCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }
    }
}
