using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectNotifications.ProjectCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Create;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ResultViewModel<int>>
{
    private readonly IProjectRepository _repository;
    private readonly IMediator _mediator;

    public CreateProjectHandler(IProjectRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        await _repository.Create(project, cancellationToken);

        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);

        await _mediator.Publish(projectCreated, cancellationToken);

        return ResultViewModel<int>.Success(project.Id);
    }
}