using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class SkillsViewModel
{
    public int Id { get; private set; }
    public string Description { get; private set; }

    public SkillsViewModel(int id, string description)
    {
        Id = id;
        Description = description;
    }

    public static SkillsViewModel FromEntity(Skill skill) => new(skill.Id, skill.Description);
}