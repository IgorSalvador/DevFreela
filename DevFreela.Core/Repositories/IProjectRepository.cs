using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAll(string search = "", int page = 1, int pageSize = 10, CancellationToken cancellation = default);
    Task<Project?> GetById(int id, CancellationToken cancellation);
    Task<Project?> GetDetailsById(int id, CancellationToken cancellation);
    Task<bool> Exists(int id, CancellationToken cancellation);
    Task<int> Create(Project project, CancellationToken cancellation);
    Task CreateComment(ProjectComment comment, CancellationToken cancellation);
    Task Update(Project project, CancellationToken cancellation);
}