using RegistaMaster.Domain.DTOModels.Entities.ErrorLogModels;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IErrorLogRepository: IRepository
  {
    IQueryable<ErrorLogDTO> GetList();
    public Task<string> AddErrorLog(ErrorLog model);
  }
}
