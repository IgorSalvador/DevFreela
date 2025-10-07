using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface ISkillRepository
{
    Task<List<Skill>> GetAll(string search = "", int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task Create(Skill skill, CancellationToken cancellationToken);
}