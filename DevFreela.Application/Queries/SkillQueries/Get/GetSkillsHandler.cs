using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.Get;

public class GetSkillsHandler : IRequestHandler<GetSkillsQuery, ResultViewModel<List<SkillsViewModel>>>
{
    private readonly ISkillRepository _repository;

    public GetSkillsHandler(ISkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<SkillsViewModel>>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _repository.GetAll(request.Search, request.Page, request.PageSize, cancellationToken);

        var skillViewModels = skills.Select(SkillsViewModel.FromEntity).ToList();

        return ResultViewModel<List<SkillsViewModel>>.Success(skillViewModels);
    }
}