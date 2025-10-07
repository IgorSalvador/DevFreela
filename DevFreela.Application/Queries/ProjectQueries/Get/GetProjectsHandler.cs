using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.Get;

public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly IProjectRepository _repository;

    public GetProjectsHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAll(request.Search, request.Page, request.PageSize, cancellationToken);

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}