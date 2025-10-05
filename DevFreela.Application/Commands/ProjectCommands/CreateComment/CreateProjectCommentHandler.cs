using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.CreateComment;

public class CreateProjectCommentHandler : IRequestHandler<CreateProjectCommentCommand, ResultViewModel>
{
    private readonly AppDbContext _context;

    public CreateProjectCommentHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == request.IdProject, cancellationToken);

        if (project is null) ResultViewModel.Error("Project not found!");

        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

        await _context.ProjectComments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultViewModel.Success();
    }
}