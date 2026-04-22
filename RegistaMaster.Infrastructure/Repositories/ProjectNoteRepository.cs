using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ProjectNoteModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class ProjectNoteRepository : Repository, IProjectNoteRepository
  {
    private readonly RegistaMasterContext context;
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    public ProjectNoteRepository(RegistaMasterContext _context, IUnitOfWork _unitOfWork, SessionModel _session): base(_context,_session)
    {
      context = _context;
      unitOfWork = _unitOfWork;
      session = _session;
    }
    public async Task<string> CheckRequest(int id)
    {
      try
      {
        if (unitOfWork.Repository.GetNonDeletedAndActive<Request>(t => t.ProjectID == id && t.RequestStatus != RequestStatus.Closed).Count() != 0)
          return "-1";
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public List<SelectListItem> CreatedBySelectList()
    {
      var list= GetNonDeletedAndActive<User>(t=>true).Select(user => new SelectListItem
      {
        Value = user.ID.ToString(),
        Text = user.Fullname,
      }).ToList();
      return list;  
    }

    public void Delete(int id)
    {
      var project = GetNonDeletedAndActive<ProjectNote>(t => t.ID == id);
      DeleteRange(project.ToList());
      Delete<ProjectNote>(id);
    }

    public async Task<string> DeleteNoteWithProjectId(int id)
    {
      var project = GetNonDeletedAndActive<ProjectNote>(t => t.ProjectID == id);
      await DeleteRange(project.ToList());
      return "1";
    }

    public async Task<IQueryable<ProjectNoteDTO>> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<ProjectNote>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new ProjectNoteDTO()
        {
          ID = s.ID,
          Date = s.Date,
          Description = s.Description,
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetProject()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> ResponsibleHeler = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Projects.Where(t => true);
        foreach(var item in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            ID = item.ID,
            Name = item.ProjectName,
          };ResponsibleHeler.Add(helper);
        }
        return ResponsibleHeler;
      }
      catch (Exception e) 
      {

        throw e;
      }
    }

    public async Task<string> GetProjectNotes(int id)
    {
      try
      {
        var notes = GetNonDeletedAndActive<ProjectNote>(t => t.ProjectID == id).Select(p => new ProjectNoteDTO
        {
          Date = p.Date,
          Description = p.Description,
          ID = p.ID,
          NoteType = p.NoteType,
          CreatedBy = p.CreatedBy,
        });
        return JsonConvert.SerializeObject(notes);
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> ProjectNoteAdd(ProjectNote model)
    {
      try
      {
        model.Date = DateTime.Now;
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> UpdateProjectNote(ProjectNoteDTO model)
    {
      var note = await GetById<ProjectNote>(model.ID);
      note.NoteType = model.NoteType;
      note.Description = model.Description;
      Update(note);
      await unitOfWork.SaveChanges();
      return "1";
    }
  }
}
