using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.HealthChecksModel;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class HealthCheckRepository : Repository, IHealthCheckRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;
    public HealthCheckRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      session = _session;
      unitOfWork = _unitOfWork;
    }

    public async Task<string> AddHealthCheck(HealthCheck model)
    {
      try
      {
        model.RequestDate = DateTime.Now;
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public IQueryable<HealthCheckDTO> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<HealthCheck>(
          t=>t.ObjectStatus==ObjectStatus.NonDeleted && 
          t.Status==Status.Active).
          Select(t => new HealthCheckDTO
        {
          Status = t.RequestStatus,
          RequestDate = t.RequestDate,
          RequestDesc = t.RequestDesc,
        });
      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}
