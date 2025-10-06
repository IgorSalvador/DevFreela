using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.CreateSkill;

public class CreateUserSkillCommand : IRequest<ResultViewModel>
{
    public int Id { get; set; }
    public int[] Skills { get; set; }

    public CreateUserSkillCommand(int id, int[] skills)
    {
        Id = id;
        Skills = skills;
    }

    public List<UserSkill> ToEntities()
    {
        return Skills.Select(s => new UserSkill(Id, s)).ToList();
    }
}