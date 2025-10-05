using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.ProjectQueries.GetById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    private readonly AppDbContext _context;

    public GetProjectByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return project is null
            ? ResultViewModel<ProjectViewModel>.Error("Project not found!")
            : ResultViewModel<ProjectViewModel>.Success(ProjectViewModel.FromEntity(project));
    }
}