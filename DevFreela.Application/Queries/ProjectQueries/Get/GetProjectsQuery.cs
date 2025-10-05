using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.Get;

public class GetProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
{
    public string Search { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetProjectsQuery(string search, int page = 1, int pageSize = 10)
    {
        Search = search;
        Page = page;
        PageSize = pageSize;
    }
}