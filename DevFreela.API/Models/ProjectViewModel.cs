using DevFreela.API.Entities;

namespace DevFreela.API.Models;

public class ProjectViewModel
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int IdClient { get; private set; }
    public int IdFreelancer { get; private set; }
    public string ClientName { get; private set; }
    public string FreelancerName { get; private set; }
    public decimal TotalCost { get; private set; }
    public List<string> Comments { get; set; }

    public ProjectViewModel(int id, string title, string description, int idClient, string clientName, int idFreelancer,
        string freelancerName, decimal totalCost, List<ProjectComment> comments)
    {
        Id = id;
        Title = title;
        Description = description;
        ClientName = clientName;
        FreelancerName = freelancerName;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;
        Comments = comments.Select(x => x.Content).ToList();
    }

    public static ProjectViewModel FromEntity(Project entity)
        => new(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.IdClient,
            entity.Client.FullName,
            entity.IdFreelancer,
            entity.Freelancer.FullName,
            entity.TotalCost,
            entity.Comments
        );
}