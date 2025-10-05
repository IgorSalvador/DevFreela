using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.Update;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly AppDbContext _context;

    public UpdateProjectHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == request.IdProject, cancellationToken);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.Update(request.Title, request.Description, request.TotalCost);

        _context.Projects.Update(project);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel.Success();
    }
}