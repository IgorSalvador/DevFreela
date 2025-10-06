using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.Create;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    private readonly AppDbContext _context;

    public CreateUserHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ResultViewModel<int>(user.Id);
    }
}