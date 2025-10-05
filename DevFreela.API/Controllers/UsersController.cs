using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(x => x.Skills)
                .ThenInclude(x => x.Skill)
                .SingleOrDefault(x => x.Id == id);

            if (user is null)
                return NotFound();

            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = new User(model.FullName, model.Email, model.BirthDate);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("{id:int}/skills")]
        public IActionResult PostSkill(int id, UserSkillsInputModel model)
        {
            var usersSkills = model.Skills.Select(s => new UserSkill(id, s)).ToList();

            _context.UserSkills.AddRange(usersSkills);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Process image

            return Ok(description);
        }
    }
}
