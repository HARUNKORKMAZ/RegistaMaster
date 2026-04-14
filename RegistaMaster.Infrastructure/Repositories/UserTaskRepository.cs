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
          Id = s.Id,
          Subject = s.Subject,
          Description = s.Description,
          Category = s.Category,
          CetegoryId = s.CetegoryId,
          PageUrl = s.PageUrl,
          RequestStatus = s.RequestStatus,
          VersionId = s.VersionId,
          ProjetId = s.ProjetId,
          ModuleId = s.ModuleId,
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
        var userTask= await GetById<UserTask>(model.Id);
        userTask.Subject = model.Subject;
        userTask.Description = model.Description;
        userTask.Category = model.Category;
        userTask.CetegoryId = model.CetegoryId;
        userTask.PageUrl = model.PageUrl;
        userTask.RequestStatus = model.RequestStatus;
        userTask.VersionId = model.VersionId;
        userTask.ProjetId = model.ProjetId;
        userTask.ModuleId = model.ModuleId;
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
