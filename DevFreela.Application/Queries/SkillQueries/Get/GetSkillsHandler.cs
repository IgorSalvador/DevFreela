using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.SkillQueries.Get;

public class GetSkillsHandler : IRequestHandler<GetSkillsQuery, ResultViewModel<List<SkillsViewModel>>>
{
    private readonly AppDbContext _context;

    public GetSkillsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<List<SkillsViewModel>>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _context.Skills
            .Where(s => s.Description.Contains(request.Search))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var skillViewModels = skills.Select(SkillsViewModel.FromEntity).ToList();

        return ResultViewModel<List<SkillsViewModel>>.Success(skillViewModels);
    }
}