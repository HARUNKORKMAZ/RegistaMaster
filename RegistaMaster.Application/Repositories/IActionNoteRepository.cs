using RegistaMaster.Domain.DTOModels.Entities.ActionNoteModels;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IActionNoteRepository : IRepository
  {
    IQueryable<ActionNoteDTO> GetList(int Id);
    Task<string> AddActionNote(ActionNote model);
    Task<string> ActionNoteUpdate(ActionNote model);
    string Delete(int Id);


  }
}
