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

        var action = GetNonDeletedAndActive<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        chart.ActionNotStarted = action.Where(t => t.ActionStatus == ActionStatus.NotStarted).Count();
        chart.ActionContinued = action.Where(t => t.ActionStatus == ActionStatus.Continued).Count();
        chart.ActionCompleted = action.Where(t => t.ActionStatus == ActionStatus.Completed).Count();
        chart.ActionCancel = action.Where(t => t.ActionStatus == ActionStatus.Canceled).Count();
        
        
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
        var users = GetQueryable<User>(t => t.AuthorizationStatus != AuthorizationStatus.Admin).Select(t => new UserChartDTO
        {
          NotStarted=actions.Where(t=>t.ActionStatus == ActionStatus.NotStarted && t.ResponsibleId==t.Id).Count(),
          Continued=actions.Where(t=>t.ActionStatus == ActionStatus.Continued && t.ResponsibleId==t.Id).Count(),
          Completed=actions.Where(t=>t.ActionStatus == ActionStatus.Completed && t.ResponsibleId==t.Id).Count(),
          Cancel=actions.Where(t=>t.ActionStatus == ActionStatus.Canceled && t.ResponsibleId==t.Id).Count(),
          UserFullName=t.FullName
        }).ToList();
        return users;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<UserChartDTO> DeveloperChart(int Id)
    {
      try
      {
        var userAction = GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.ResponsibleId == Id);
        var user = new UserChartDTO()
        {
          NotStarted = userAction.Where(t => t.ActionStatus == ActionStatus.NotStarted).Count(),
          Continued = userAction.Where(t => t.ActionStatus == ActionStatus.Continued).Count(),
          Completed = userAction.Where(t => t.ActionStatus == ActionStatus.Completed).Count(),
          Cancel = userAction.Where(t => t.ActionStatus == ActionStatus.Canceled).Count(),
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
        var model = GetQueryable<Action>(t=>(t.ResponsibleId==session.Id || t.CreatedBy==session.Id) && t.ObjectStatus==ObjectStatus.NonDeleted).Select(s=> new ActionDTO()
        {
          Id=s.Id,
          Description=s.Description,
          EndDate=s.EndDate,
          OpeningDate=s.OpeningDate,
          ResponsibleId=s.ResponsibleId,
          ActionStatus=s.ActionStatus,
          Subject=s.Subject,
          RequestId=s.RequestId,
          ActionPriorityStatus=s.ActionPriorityStatus,
          LastModifiedBy=s.LastModifiedBy,
          CreatedBy=s.CreatedBy,
          StartDate=s.StartDate,
          CompleteDate=s.ComplateDate
        }).OrderByDescending(s=>s.Id).ToList();
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
        chart.ActionNotStarted = action.Where(t => t.ActionStatus == ActionStatus.NotStarted).Count();
        chart.ActionContinued = action.Where(t => t.ActionStatus == ActionStatus.Continued).Count();
        chart.ActionCompleted = action.Where(t => t.ActionStatus == ActionStatus.Completed).Count();
        chart.ActionCancel = action.Where(t => t.ActionStatus == ActionStatus.Canceled).Count();


        var userActions = GetQueryable<Action>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.ResponsibleId == Id);
        var user = new UserChartDTO()
        {
          NotStarted = userActions.Where(t => t.ActionStatus == ActionStatus.NotStarted).Count(),
          Continued = userActions.Where(t => t.ActionStatus == ActionStatus.Continued).Count(),
          Completed = userActions.Where(t => t.ActionStatus == ActionStatus.Completed).Count(),
          Cancel = userActions.Where(t => t.ActionStatus == ActionStatus.Canceled).Count(),
        };
        chart.userChartDTO = user;
        return chart;
      }
      catch (Exception)
      {

        throw;
      }
    }
  }
}
