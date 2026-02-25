using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using RegistaMaster.Domain.DTOModels.Entities.RequestModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;

namespace RegistaMaster.Application.Repositories
{
  public interface IRequestRepository : IRepository
  {
    Task<Request> RequestAdd(Request model);
    Task<Request> UpdateRequest(RequestGridDTO model);
    void Delete(int Id);
    Task<List<RequestGridDTO>> GetList();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetProject();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetCustomer();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetModeluSelect();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetVersionSelect();
    Task<List<ActionDTO>> GetActionDetail(int RequestId);
    Task<List<RequestGridDTO>> GetListWithFiles();
    Task<string> ActionStatusChangeUpdate(int Id, ActionStatus actionStatus);
    Task<List<SelectListItem>> NotificationTypeSelectList();
    Task<List<SelectListItem>> CategorySelectList();
    Task<List<SelectListItem>> GetProjectSelect();
    Task<List<SelectListItem>> GetModule();
    Task<List<SelectListItem>> GetModuleList(int id);
    Task<List<SelectListItem>> GetVersion();
    Task<List<SelectListItem>> ResponsibleSelecetList();
    Task<List<SelectListItem>> GetVersionList(int id);
    Task<string> CompleteRequest(int id);
    Task<string> AddRequestFiles(List<IFormFile> file, int Id);
    Task<string> DeleteFilesWithRequestID(int id);
    Task<string> RequestDeleteWithActions(int id);
    Task<string> RequestDelete(int id);
















  }
}
