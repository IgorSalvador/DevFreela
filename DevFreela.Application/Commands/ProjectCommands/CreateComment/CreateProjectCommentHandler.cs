using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CreateComment;

public class CreateProjectCommentHandler : IRequestHandler<CreateProjectCommentCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;

    public CreateProjectCommentHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.Exists(request.IdProject, cancellationToken);

        if (!exists) return ResultViewModel.Error("Project not found!");

        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

        await _repository.CreateComment(comment, cancellationToken);

        return ResultViewModel.Success();
    }
}