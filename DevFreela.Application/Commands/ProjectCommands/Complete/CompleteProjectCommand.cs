using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Complete;

public class CompleteProjectCommand : IRequest<ResultViewModel>
{
    public int Id { get; set; }

    public CompleteProjectCommand(int id)
    {
        Id = id;
    }
}