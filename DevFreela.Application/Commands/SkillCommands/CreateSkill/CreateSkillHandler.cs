using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.CreateSkill;

public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, ResultViewModel>
{
    private readonly ISkillRepository _repository;

    public CreateSkillHandler(ISkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = request.ToEntity();

        await _repository.Create(skill, cancellationToken);

        return ResultViewModel.Success();
    }
}