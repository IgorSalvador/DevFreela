﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Start;

public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;

    public StartProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetById(request.Id, cancellationToken);

        if (project is null) return ResultViewModel.Error("Project not found!");

        project!.Start();

        await _repository.Update(project, cancellationToken);

        return ResultViewModel.Success();
    }
}