using Microsoft.AspNetCore.Mvc.Rendering;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using RegistaMaster.Domain.DTOModels.Entities.ActionNoteModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using Action = RegistaMaster.Domain.Entities.Action;


namespace RegistaMaster.Application.Repositories
{
  public interface IActionRepository: IRepository
  {
    IQueryable<ActionDTO> GetList();
    Task<string> ActionUpdate(ActionDTO model);
    string Delete(int Id);
    Task<ActionPageDTO> GetAction(int Id);
    Task<List<SelectListItem>> ResponsibleHelerModelList();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetRequest();
    Task<List<SelectListItem>> ActionPriorityStatusList();
    List<ActionDTO> GetActionsByRequestcId(int Id);
    Task<string> ChangeActionStatus(ActionPageDTO model);
    Task<string> ActionNoteUpdate(ActionNoteDTO model);
    Task<string> ActionDelete(int Id);
    Task<string> AddAction(Action model);
  }
}
