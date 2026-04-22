using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.UserLogModel;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class UserLogRepository : Repository, IUserLogRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;
    public UserLogRepository(RegistaMasterContext _context, SessionModel _session,IUnitOfWork _unitOfWork) : base(_context, _session)
    {
        context = _context;
        session = _session;
        unitOfWork = _unitOfWork;
    }

    public async Task<string> AddUserLog(UserLog model)
    {
      try
      {
        model.LoginDate = DateTime.Now;
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public IQueryable<UserLogDTO> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<UserLog>(
          t => t.ObjectStatus == ObjectStatus.NonDeleted && 
          t.Status == Status.Active).
          Select(s => new UserLogDTO()
        {
          NameSurname = s.NameSurname,
          LoginDate = s.LoginDate,
          ClientID = s.ClientID,
          MemberID = s.MemberID,
        });
      }
      catch (Exception)
      {

        throw;
      }
    }
  }
}
