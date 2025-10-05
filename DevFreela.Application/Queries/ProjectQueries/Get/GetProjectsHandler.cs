using Azure;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.ProjectQueries.Get;

public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly AppDbContext _context;

    public GetProjectsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted &&
                        (request.Search == string.Empty || x.Title.Contains(request.Search) || x.Description.Contains(request.Search)))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}