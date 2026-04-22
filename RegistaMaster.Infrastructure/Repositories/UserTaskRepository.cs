using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class UserTaskRepository : Repository, IUserTaskRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;
    public UserTaskRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      session = _session;
      unitOfWork = _unitOfWork;
    }

    public async Task<string> AddUserTask(UserTask model)
    {
      try
      {
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> DeleteUserTask(int id)
    {
      try
      {
        await unitOfWork.Repository.Delete<UserTask>(id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IQueryable<UserTask>> GetList()
    {
      try
      {
        return  GetNonDeletedAndActive<UserTask>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new UserTask()
        {
          ID = s.ID,
          Subject = s.Subject,
          Description = s.Description,
          Category = s.Category,
          CetegoryID = s.CetegoryID,
          PageUrl = s.PageUrl,
          RequestStatus = s.RequestStatus,
          VersionID = s.VersionID,
          ProjectID = s.ProjectID,
          ModuleID = s.ModuleID,
          StartDate = s.StartDate,
          PlannedEndDate = s.PlannedEndDate,
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> UpdateUserTask(UserTask model)
    {
      try
      {
        var userTask= await GetById<UserTask>(model.ID);
        userTask.Subject = model.Subject;
        userTask.Description = model.Description;
        userTask.Category = model.Category;
        userTask.CetegoryID = model.CetegoryID;
        userTask.PageUrl = model.PageUrl;
        userTask.RequestStatus = model.RequestStatus;
        userTask.VersionID = model.VersionID;
        userTask.ProjectID = model.ProjectID;
        userTask.ModuleID = model.ModuleID;
        userTask.StartDate = model.StartDate;
        userTask.PlannedEndDate = model.PlannedEndDate;
        Update(userTask);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
