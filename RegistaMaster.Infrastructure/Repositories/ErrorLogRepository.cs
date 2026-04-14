using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ErrorLogModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class ErrorLogRepository : Repository, IErrorLogRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;
    public ErrorLogRepository(RegistaMasterContext _context, SessionModel _session,IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      session = _session;
      unitOfWork = _unitOfWork;
    }

    public async Task<string> AddErrorLog(ErrorLog model)
    {
      try
      {
        model.ErrorDate = DateTime.Now;
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();  
        return "1";
      }
      catch (Exception e)
      {
        throw e; 
      }
    }

    public IQueryable<ErrorLogDTO> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<ErrorLog>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active).Select(s => new ErrorLogDTO()
        {
          NameSurname = s.NameSurname,
          ErrorDate = s.ErrorDate,
          ErrorDesc = s.ErrorDesc,
          ClientId = s.ClientId,
          MemberId = s.MemberId,
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
