using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetById;

public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
{
    public int Id { get; set; }

    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }
}