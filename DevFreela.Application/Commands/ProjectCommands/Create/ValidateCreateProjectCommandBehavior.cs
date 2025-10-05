using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.Create;

public class ValidateCreateProjectCommandBehavior : 
    IPipelineBehavior<CreateProjectCommand, ResultViewModel<int>>
{
    private readonly AppDbContext _context;

    public ValidateCreateProjectCommandBehavior(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, 
        CancellationToken cancellationToken)
    {
        var clientExists = await _context.Users.AnyAsync(x => x.Id == request.IdClient, cancellationToken);
        var freelancerExists = await _context.Users.AnyAsync(x => x.Id == request.IdFreelancer, cancellationToken);

        if (!clientExists || !freelancerExists) return ResultViewModel<int>.Error("Customer or freelancer does not exist.");

        return await next(cancellationToken);
    }
}