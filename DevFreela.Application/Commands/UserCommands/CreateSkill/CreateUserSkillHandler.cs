using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.CreateSkill;

public class CreateUserSkillHandler : IRequestHandler<CreateUserSkillCommand, ResultViewModel>
{
    private readonly AppDbContext _context;

    public CreateUserSkillHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    {
        var userSkills = request.ToEntities();

        await _context.UserSkills.AddRangeAsync(userSkills, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel.Success();
    }
}