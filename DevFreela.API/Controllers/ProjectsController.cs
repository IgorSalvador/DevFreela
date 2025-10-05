using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "", int page = 1, int pageSize = 5)
        {
            var result = await _projectService.GetAll(search, page, pageSize);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _projectService.GetById(id);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProjectInputModel model)
        {
            var result = await _projectService.Create(model);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateProjectInputModel model)
        {
            model.IdProject = id;

            var result = await _projectService.Update(model);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projectService.Delete(id);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id:int}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _projectService.Start(id);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id:int}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _projectService.Complete(id);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("{id:int}/comments")]
        public async Task<IActionResult> PostComment(int id, CreateProjectCommentInputModel model)
        {
            var result = await _projectService.CreateComment(id, model);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
