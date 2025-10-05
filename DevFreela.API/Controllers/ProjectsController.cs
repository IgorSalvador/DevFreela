using DevFreela.Application.Commands.ProjectCommands.Complete;
using DevFreela.Application.Commands.ProjectCommands.Create;
using DevFreela.Application.Commands.ProjectCommands.CreateComment;
using DevFreela.Application.Commands.ProjectCommands.Delete;
using DevFreela.Application.Commands.ProjectCommands.Start;
using DevFreela.Application.Commands.ProjectCommands.Update;
using DevFreela.Application.Queries.ProjectQueries.Get;
using DevFreela.Application.Queries.ProjectQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "", int page = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetProjectsQuery(search, page, pageSize), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProjectCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand model, CancellationToken cancellationToken = default)
        {
            model.IdProject = id;

            var result = await _mediator.Send(model, cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id:int}/start")]
        public async Task<IActionResult> Start(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new StartProjectCommand(id), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id:int}/complete")]
        public async Task<IActionResult> Complete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id), cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("{id:int}/comments")]
        public async Task<IActionResult> PostComment(int id, CreateProjectCommentCommand model, CancellationToken cancellationToken = default)
        {
            model.IdProject = id;

            var result = await _mediator.Send(model, cancellationToken);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
