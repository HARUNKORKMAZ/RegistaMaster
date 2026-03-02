using RegistaMaster.Domain.DTOModels.SecurityModels;

namespace RegistaMaster.Application.Repositories
{
  public interface IUnitOfWork
  {
    IRepository Repository { get; }
    IActionRepository ActionRepository { get; }
    IActionNoteRepository ActionNoteRepository { get; }
    IHomeRepository HomeRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IUserRepository UserRepository { get; }
    IRequestRepository RequestRepository { get; }
    IProjectRepository ProjectRepository { get; }
    IVersionRepository VersionRepository { get; }
    IModuleRepository ModuleRepository { get; }



    Task<int> SaveChanges();
    SessionModel GetSession();
  }
}
