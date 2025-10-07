using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    private readonly IProjectRepository _repository;

    public GetProjectByIdHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetDetailsById(request.Id, cancellationToken);

        return project is null
            ? ResultViewModel<ProjectViewModel>.Error("Project not found!")
            : ResultViewModel<ProjectViewModel>.Success(ProjectViewModel.FromEntity(project));
    }
}