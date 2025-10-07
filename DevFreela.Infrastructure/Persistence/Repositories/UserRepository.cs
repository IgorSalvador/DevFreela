using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.Skills)
            .ThenInclude(x => x.Skill)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> Create(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task CreateSkill(List<UserSkill> skills, CancellationToken cancellationToken)
    {
        await _context.UserSkills.AddRangeAsync(skills, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}