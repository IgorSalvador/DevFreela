using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "", int page = 1, int pageSize = 5)
        {
            var projects = _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .Where(x => !x.IsDeleted && 
                            (search == string.Empty || x.Title.Contains(search) || x.Description.Contains(search)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .Include(x => x.Comments)
                .SingleOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, model);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            project.SetAsDeleted();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            project.Start();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            project.Complete();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id:int}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }
    }
}
