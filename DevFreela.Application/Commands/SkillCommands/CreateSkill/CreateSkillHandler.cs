using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.CreateSkill;

public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, ResultViewModel>
{
    private readonly AppDbContext _context;

    public CreateSkillHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = request.ToEntity();

        await _context.Skills.AddAsync(skill, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel.Success();
    }
}