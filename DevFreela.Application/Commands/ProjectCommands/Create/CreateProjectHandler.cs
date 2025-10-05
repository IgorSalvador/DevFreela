using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Create;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ResultViewModel<int>>
{
    private readonly AppDbContext _context;

    public CreateProjectHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel<int>.Success(project.Id);
    }
}