using DevFreela.Application.Models;

namespace DevFreela.Application.Services;

public interface IProjectService
{
    Task<ResultViewModel<List<ProjectItemViewModel>>> GetAll(string search = "", int page = 1, int pageSize = 5);
    Task<ResultViewModel<ProjectViewModel>> GetById(int id);
    Task<ResultViewModel<int>> Create(CreateProjectInputModel model);
    Task<ResultViewModel> Update(UpdateProjectInputModel model);
    Task<ResultViewModel> Delete(int id);
    Task<ResultViewModel> Start(int id);
    Task<ResultViewModel> Complete(int id);
    Task<ResultViewModel> CreateComment(int id, CreateProjectCommentInputModel model);
}