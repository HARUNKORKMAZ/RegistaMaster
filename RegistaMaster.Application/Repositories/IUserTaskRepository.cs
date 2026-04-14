using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IUserTaskRepository: IRepository
  {
    Task<string> AddUserTask(UserTask model);
    Task<string> UpdateUserTask(UserTask model);
    Task<IQueryable<UserTask>> GetList();
    Task<string> DeleteUserTask(int id);
  }
}
