using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.CreateSkill;

public class CreateUserSkillHandler : IRequestHandler<CreateUserSkillCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public CreateUserSkillHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    {
        var userSkills = request.ToEntities();

        await _repository.CreateSkill(userSkills, cancellationToken);

        return ResultViewModel.Success();
    }
}