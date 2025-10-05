using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectNotifications.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Create;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ResultViewModel<int>>
{
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public CreateProjectHandler(AppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);

        await _mediator.Publish(projectCreated, cancellationToken);

        return ResultViewModel<int>.Success(project.Id);
    }
}