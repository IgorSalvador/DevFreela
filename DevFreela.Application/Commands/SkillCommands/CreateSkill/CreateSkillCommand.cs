using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.CreateSkill;

public class CreateSkillCommand : IRequest<ResultViewModel>
{
    public string Description { get; private set; }

    public CreateSkillCommand(string description)
    {
        Description = description;
    }

    public Skill ToEntity() => new(Description);
}