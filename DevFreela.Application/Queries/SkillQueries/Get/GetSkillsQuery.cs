using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.Get;

public class GetSkillsQuery : IRequest<ResultViewModel<List<SkillsViewModel>>>
{
    public string Search { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetSkillsQuery(string search, int page = 1, int pageSize = 10)
    {
        Search = search;
        Page = page;
        PageSize = pageSize;
    }
}