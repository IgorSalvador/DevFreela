using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.Start;

public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
{
    private readonly AppDbContext _context;

    public StartProjectHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.Start();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel.Success();
    }
}