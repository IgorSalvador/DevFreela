using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.Create;

public class CreateProjectCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }

    public CreateProjectCommand(string title, string description, int idClient, int idFreelancer, decimal totalCost)
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;
    }

    public Project ToEntity() => new(Title, Description, IdClient, IdFreelancer, TotalCost);
}