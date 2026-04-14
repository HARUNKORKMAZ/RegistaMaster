using Microsoft.AspNetCore.Mvc.Rendering;
using RegistaMaster.Domain.DTOModels.Entities.ProjectNoteModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IProjectNoteRepository  : IRepository
  {
    Task<string> ProjectNoteAdd(ProjectNote model);
    Task<string> UpdateProjectNote(ProjectNoteDTO model);
    void Delete(int id);
    Task<IQueryable<ProjectNoteDTO>> GetList();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetProject();
    Task<string> DeleteNoteWithProjectId(int id);
    List<SelectListItem> CreatedBySelectList();
    Task<string> GetProjectNotes(int id);
    Task<string> CheckRequest(int id);

  }
}
