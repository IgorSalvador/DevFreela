using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.Delete;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    private readonly AppDbContext _context;

    public DeleteProjectHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (project is null) ResultViewModel.Error("Project not found!");

        project!.SetAsDeleted();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel.Success();
    }
}