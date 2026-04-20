using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using RegistaMaster.Domain.DTOModels.Entities.ActionNoteModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using Action = RegistaMaster.Domain.Entities.Action;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class ActionRepository : Repository, IActionRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;

    public ActionRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      session = _session;
      unitOfWork = _unitOfWork;
    }

    public async Task<string> ActionDelete(int Id)
    {
      try
      {
        var actionNotes = GetNonDeletedAndActive<ActionNote>(t => t.ActionId == t.Id).ToList();
        await unitOfWork.Repository.DeleteRange(actionNotes);
        await unitOfWork.Repository.Delete<Action>(Id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> ActionNoteUpdate(ActionNoteDTO model)
    {
      try
      {
        var actionNote = await GetById<ActionNote>(model.Id);
        actionNote.Description = model.Description;
        actionNote.Title = model.Title;
        unitOfWork.Repository.Update(actionNote);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<SelectListItem>> ActionPriorityStatusList()
    {
      try
      {
        var list = GetEnumSelect<ActionPriorityStatus>().Select(aps => new SelectListItem
        {
          Value = aps.Id.ToString(),
          Text = aps.Text,
        });
        return list.ToList();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> ActionUpdate(ActionDTO model)
    {
      try
      {
        var action = await GetById<Action>(model.Id);
        action.Subject = model.Subject;
        action.Description = model.Description;
        action.ActionPriorityStatus = model.ActionPriorityStatus;
        action.ResponsibleId = model.ResponsibleId;
        action.OpeningDate = model.OpeningDate;
        action.EndDate = model.EndDate;
        unitOfWork.Repository.Update(action);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> AddAction(Action model)
    {
      try
      {
        var request = await GetById<Request>(model.RequestId);
        if (request.RequestStatus == RequestStatus.Waiting)
        {
          var cancelledActions = GetQueryable<Action>(t => t.RequestId == model.RequestId && t.Status == Status.Active && t.ObjectStatus == ObjectStatus.NonDeleted && t.ActionStatus == ActionStatus.Canceled).ToList();
          foreach (var action in cancelledActions)
          {
            action.Status = Status.Active;
          }
          await UpdateRange(cancelledActions);
          request.RequestStatus = RequestStatus.Start;
          Update(request);
          await unitOfWork.SaveChanges();
        }
        model.ActionStatus = ActionStatus.NotStarted;
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> ChangeActionStatus(ActionPageDTO model)
    {
      try
      {
        var action = await GetById<Action>(model.Id);
        action.ActionStatus = model.ActionStatus;
        action.StartDate = model.StartDate;
        action.ComplateDate = model.CompleteDate;
        unitOfWork.Repository.Update(action);
        await unitOfWork.SaveChanges();

        var request = await GetById<Request>(action.RequestId);

        var requestActions = GetQueryable<Action>(t => t.RequestId == action.RequestId && t.Status == Status.Active && t.ActionStatus != ActionStatus.Completed);

        var cancelledActions = requestActions.Where(x => x.ActionStatus == ActionStatus.Canceled).Count();

        var waitingActions = requestActions.Where(x => x.ActionStatus == ActionStatus.Continued || x.ActionStatus == ActionStatus.NotStarted).Count();

        if (cancelledActions > 0 && waitingActions == 0)
        {
          request.RequestStatus = RequestStatus.Waiting;
          unitOfWork.Repository.Update(request);
          await unitOfWork.SaveChanges();
          return "2";
        }

        var countiunedActions = requestActions.Where(x => x.ActionStatus == ActionStatus.Continued).Count();

        if(request.RequestStatus != RequestStatus.Start && countiunedActions > 0)
        {
          request.RequestStatus = RequestStatus.Start;
          unitOfWork.Repository.Update(request);
          await unitOfWork.SaveChanges();
          return "2";
        }



        if (requestActions.Count() == 0)
        {
          request.RequestStatus = RequestStatus.Closed;
          request.PlanedEndDate = DateTime.Now;
          unitOfWork.Repository.Update(request);
          await unitOfWork.SaveChanges();
          return "2";
        }
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public string Delete(int Id)
    {
      var action = GetNonDeletedAndActive<Action>(t => t.Id == Id);
      DeleteRange(action.ToList());
      Delete<Action>(Id);
      return "1";
    }

    public async Task<ActionPageDTO> GetAction(int Id)
    {
      try
      {
        return await GetQueryable<Action>(t=>t.Id==Id && t.ObjectStatus == ObjectStatus.NonDeleted).Select(s=>new ActionPageDTO
        {
          Id = s.Id,
          Responsible = s.Repsonsible.Fullname,
          OpeningDate = s.OpeningDate,
          EndDate = s.EndDate,
          Description = s.Description,
          ActionStatus = s.ActionStatus,
          RequestId = s.RequestId,
        }).FirstOrDefaultAsync();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public List<ActionDTO> GetActionsByRequestcId(int Id)
    {
      try
      {
        var model = GetQueryable<Action>(t => t.RequestId == Id && t.ObjectStatus == ObjectStatus.NonDeleted).OrderByDescending(t => t.Id);

        List<ActionDTO> actionList = new List<ActionDTO>();


        foreach (var item in model)
        {
          ActionDTO actions = new ActionDTO()
          {
            Id = item.Id,
            Description = item.Description,
            EndDate = item.EndDate,
            OpeningDate = item.OpeningDate,
            ResponsibleId = item.ResponsibleId,
            ActionStatus = item.ActionStatus,
            ActionPriorityStatus = item.ActionPriorityStatus,
            Subject = item.Subject,
            LastModifiedBy = item.LastModifiedBy,
            RequestId = Id,
            CreateOn = item.CreatedOn,
            CreatedBy = item.CreatedBy,
            StartDate = item.StartDate,
            CompleteDate = item.ComplateDate,
          };
          actionList.Add(actions);
        }
        return actionList;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public IQueryable<ActionDTO> GetList()
    {
      try
      {
        return GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new ActionDTO()
        {
          Id = s.Id,
          Description = s.Description,
          EndDate = s.EndDate,
          OpeningDate = s.OpeningDate,
          ResponsibleId = s.ResponsibleId,
          ActionStatus = s.ActionStatus,
          Subject = s.Subject,
          RequestId= s.RequestId,
          ActionPriorityStatus = s.ActionPriorityStatus,
          CreatedBy = s.CreatedBy,
          StartDate = s.StartDate,
          CompleteDate = s.ComplateDate
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetRequest()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> RequestHelper = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Requests.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        foreach(var item in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = item.Id,
            Name = item.Subject
          };
          RequestHelper.Add(helper);
        }
          return RequestHelper;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public  async Task<List<SelectListItem>> ResponsibleHelerModelList()
    {
      try
      {
        return GetNonDeletedAndActive<User>(t => true).Select(s => new SelectListItem
        {
          Value = s.Id.ToString(),
          Text = s.Name + " " + s.Surname
        }).ToList();
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
