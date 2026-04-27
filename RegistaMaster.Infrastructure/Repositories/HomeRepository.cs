using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.ChartModels;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using Action = RegistaMaster.Domain.Entities.Action;
using Request = RegistaMaster.Domain.Entities.Request;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class HomeRepository : Repository, IHomeRepository
  {
    private readonly RegistaMasterContext context;  
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;

    public HomeRepository(RegistaMasterContext _context, IUnitOfWork _unitOfWork, SessionModel _session) : base(_context, _session)
    {
      context = _context;
      unitOfWork = _unitOfWork;
      session = _session;
    }
    public async Task<ChartDTO> AdminChart()
    {
      try
      {
        var chart = new ChartDTO();
        var requests = GetNonDeletedAndActive<Request>(t=>true);
        chart.RequestOpen= requests.Where(t => t.RequestStatus== RequestStatus.Open).Count();
        chart.RequestStart= requests.Where(t => t.RequestStatus== RequestStatus.Start).Count();
        chart.RequestClosed= requests.Where(t => t.RequestStatus== RequestStatus.Closed).Count();
        chart.RequestWaiting= requests.Where(t => t.RequestStatus== RequestStatus.Waiting).Count();

        var action = GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        chart.ActionNotStarted = action.Where(t => t.ActionStatus == ActionStatus.notStarted).Count();
        chart.ActionContinued = action.Where(t => t.ActionStatus == ActionStatus.Contiuned).Count();
        chart.ActionCompleted = action.Where(t => t.ActionStatus == ActionStatus.Completed).Count();
        chart.ActionCancel = action.Where(t => t.ActionStatus == ActionStatus.Cancel).Count();
        
        
        return chart;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<UserChartDTO>> AdminChartUserActions()
    {
      try
      {
        var actions = GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        var users = GetNonDeletedAndActive<User>(t => t.AuthorizationStatus != AuthorizationStatus.Admin).Select(s => new UserChartDTO()
        {
          NotStarted = actions.Where(t => t.ActionStatus == ActionStatus.notStarted && t.ResponsibleID == s.ID).Count(),
          Continued = actions.Where(t => t.ActionStatus == ActionStatus.Contiuned && t.ResponsibleID == s.ID).Count(),
          Completed = actions.Where(t => t.ActionStatus == ActionStatus.Completed && t.ResponsibleID == s.ID).Count(),
          Cancel = actions.Where(t => t.ActionStatus == ActionStatus.Cancel && t.ResponsibleID == s.ID).Count(),
          UserFullName = s.Fullname
        }).ToList();

        return users;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<UserChartDTO> DeveloperChart(int Id)
    {
      try
      {
        var userAction = GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.ResponsibleID == Id);
        var user = new UserChartDTO()
        {
          NotStarted = userAction.Where(t => t.ActionStatus == ActionStatus.notStarted).Count(),
          Continued = userAction.Where(t => t.ActionStatus == ActionStatus.Contiuned).Count(),
          Completed = userAction.Where(t => t.ActionStatus == ActionStatus.Completed).Count(),
          Cancel = userAction.Where(t => t.ActionStatus == ActionStatus.Cancel).Count(),
        };
        return user;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<ActionDTO>> GetActionDTOHome()
    {
      try
      {
        var model = GetQueryable<Action>(t=>(t.ResponsibleID==session.ID || t.CreatedBy==session.ID) && t.ObjectStatus==ObjectStatus.NonDeleted).Select(s=> new ActionDTO()
        {
          ID=s.ID,
          Description=s.Description,
          EndDate=s.EndDate,
          OpeningDate=s.OpeningDate,
          ResponsibleID=s.ResponsibleID,
          ActionStatus=s.ActionStatus,
          Subject=s.Subject,
          RequestID=s.RequestID,
          ActionPriorityStatus=s.ActionPriorityStatus,
          LastModifiedBy=s.LastModifiedBy,
          CreatedBy=s.CreatedBy,
          StartDate=s.StartDate,
          CompleteDate=s.CompleteDate
        }).OrderByDescending(s=>s.ID).ToList();
        return model;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async  Task<ChartDTO> TeamLeaderChart(int Id)
    {
      try
      {
        var chart = new ChartDTO();
        var requests = GetNonDeletedAndActive<Request>(t => t.CreatedBy == Id);
        chart.RequestOpen = requests.Where(t => t.RequestStatus == RequestStatus.Open).Count();
        chart.RequestStart = requests.Where(t => t.RequestStatus == RequestStatus.Start).Count();
        chart.RequestClosed = requests.Where(t => t.RequestStatus == RequestStatus.Closed).Count();
        chart.RequestWaiting = requests.Where(t => t.RequestStatus == RequestStatus.Waiting).Count();

        var action =GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.CreatedBy == Id);
        chart.ActionNotStarted = action.Where(t => t.ActionStatus == ActionStatus.notStarted).Count();
        chart.ActionContinued = action.Where(t => t.ActionStatus == ActionStatus.Contiuned).Count();
        chart.ActionCompleted = action.Where(t => t.ActionStatus == ActionStatus.Completed).Count();
        chart.ActionCancel = action.Where(t => t.ActionStatus == ActionStatus.Cancel).Count();


        var userActions = GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.ResponsibleID == Id);
        var user = new UserChartDTO()
        {
          NotStarted = userActions.Where(t => t.ActionStatus == ActionStatus.notStarted).Count(),
          Continued = userActions.Where(t => t.ActionStatus == ActionStatus.Contiuned).Count(),
          Completed = userActions.Where(t => t.ActionStatus == ActionStatus.Completed).Count(),
          Cancel = userActions.Where(t => t.ActionStatus == ActionStatus.Cancel).Count(),
        };
        chart.UserChartDTO = user;
        return chart;
      }
      catch (Exception)
      {

        throw;
      }
    }
  }
}
