using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetById(int id, CancellationToken cancellationToken);
    Task<int> Create(User user, CancellationToken cancellationToken);
    Task CreateSkill(List<UserSkill> skills, CancellationToken cancellationToken);
}