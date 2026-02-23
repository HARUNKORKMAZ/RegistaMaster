using RegistaMaster.Domain.DTOModels.SecurityModels;

namespace RegistaMaster.Application.Repositories
{
  public interface IUnitOfWork
  {
    IRepository Repository { get; }
    IActionNoteRepository ActionNoteRepository { get; }
    IHomeRepository HomeRepository { get; }
    ICustomerRepository CustomerRepository { get; }




    Task<int> SaveChanges();
    SessionModel GetSession();
  }
}
