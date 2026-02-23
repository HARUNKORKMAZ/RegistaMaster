using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ActionNoteModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class ActionNoteRepository : Repository, IActionNoteRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;
    public ActionNoteRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      session = _session; 
      unitOfWork = _unitOfWork;
    }

    public async Task<string> ActionNoteUpdate(ActionNote model)
    {
      try
      {
        Update(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception)
      {

        throw;
      }

    }

    public async Task<string> AddActionNote(ActionNote model)
    {
      try
      {
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public string Delete(int Id)
    {
      var actionNote = GetNonDeletedAndActive<ActionNote>(t=>t.Id==Id);
      DeleteRange(actionNote.ToList());
      return "1";
    }

    public IQueryable<ActionNoteDTO> GetList(int Id)
    {
      try
      {
        return GetNonDeletedAndActive<ActionNote>(t => t.ActionId == Id).Select(s => new ActionNoteDTO
        {
          Id = s.Id,
          Title = s.Title,
          ActionId = s.ActionId,
          Description = s.Description
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
