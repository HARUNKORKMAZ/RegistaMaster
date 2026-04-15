using RegistaMaster.Domain.DTOModels.Entities.HealthChecksModel;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IHealthCheckRepository: IRepository
  {
    IQueryable<HealthCheckDTO> GetList();
    public Task<string> AddHealthCheck(HealthCheck model);
  }
}
