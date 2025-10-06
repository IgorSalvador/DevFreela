using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.UserQueries.GetById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    private readonly AppDbContext _context;

    public GetUserByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.Skills)
            .ThenInclude(x => x.Skill)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return user is null 
            ? ResultViewModel<UserViewModel>.Error("User not found!") 
            : ResultViewModel<UserViewModel>.Success(UserViewModel.FromEntity(user));
    }
}