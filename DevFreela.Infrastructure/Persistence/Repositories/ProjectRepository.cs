using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAll(string search = "", int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted &&
                        (search == string.Empty || x.Title.Contains(search) || x.Description.Contains(search)))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Project?> GetDetailsById(int id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _context.Projects.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> Create(Project project, CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }

    public async Task Update(Project project, CancellationToken cancellationToken)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateComment(ProjectComment comment, CancellationToken cancellationToken)
    {
        await _context.ProjectComments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}