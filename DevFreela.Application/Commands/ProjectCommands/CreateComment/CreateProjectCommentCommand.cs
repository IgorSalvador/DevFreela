using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CreateComment;

public class CreateProjectCommentCommand : IRequest<ResultViewModel>
{
    public string Content { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }

    public CreateProjectCommentCommand(string content, int idProject, int idUser)
    {
        Content = content;
        IdProject = idProject;
        IdUser = idUser;
    }

}