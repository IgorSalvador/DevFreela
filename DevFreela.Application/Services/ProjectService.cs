using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> GetAll(string search = "", int page = 1, int pageSize = 5)
    {
        var projects = await _context.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted &&
                        (search == string.Empty || x.Title.Contains(search) || x.Description.Contains(search)))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }

    public async Task<ResultViewModel<ProjectViewModel>> GetById(int id)
    {
        var project = await _context.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.Id == id);

        return project is null 
            ? ResultViewModel<ProjectViewModel>.Error("Project not found!") 
            : ResultViewModel<ProjectViewModel>.Success(ProjectViewModel.FromEntity(project));
    }

    public async Task<ResultViewModel<int>> Create(CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return ResultViewModel<int>.Success(project.Id);
    }

    public async Task<ResultViewModel> Update(UpdateProjectInputModel model)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == model.IdProject);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.Update(model.Title, model.Description, model.TotalCost);

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> Delete(int id)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.SetAsDeleted();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> Start(int id)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.Start();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> Complete(int id)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.Complete();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> CreateComment(int id, CreateProjectCommentInputModel model)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null) ResultViewModel.Error("Project not found!");

        var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

        await _context.ProjectComments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}