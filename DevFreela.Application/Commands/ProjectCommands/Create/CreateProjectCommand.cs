using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Create;

public class CreateProjectCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }

    public Project ToEntity() => new(Title, Description, IdClient, IdFreelancer, TotalCost);
}