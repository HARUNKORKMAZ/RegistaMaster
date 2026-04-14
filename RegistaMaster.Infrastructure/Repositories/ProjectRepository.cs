using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ProjectModels;
using RegistaMaster.Domain.DTOModels.Entities.VersionModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class ProjectRepository : Repository, IProjectRepository
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;

    public ProjectRepository(IUnitOfWork _unitOfWork, RegistaMasterContext _context, SessionModel _session) : base(_context, _session)
    {
      unitOfWork = _unitOfWork;
      context = _context;
      session = _session;
    }

    public async Task<string> AddProject(Project model)
    {
      try
      {
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        var version = new VersionDTO()
        {
          ProjectId = model.Id,
          DatabaseChange = true,
          Name = "V1.0"
        };
        await unitOfWork.VersionRepository.AddVersion(version);
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public void Delete(int ID)
    {
      var project = GetNonDeletedAndActive<Project>(t => t.Id == ID);
      DeleteRange(project.ToList());
      Delete<Project>(ID);
    }

    public async Task<string> DeleteProject(int Id)
    {
      try
      {
        await unitOfWork.VersionRepository.DeleteVersionWithProjectId(Id);
        await unitOfWork.ModuleRepository.DeleteModuleWithProjectId(Id);
        await unitOfWork.ProjectNoteRepository.DeleteNoteWithProjectId(Id);
        await unitOfWork.Repository.Delete<Project>(Id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<IQueryable<ProjectDTO>> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<Project>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new ProjectDTO()
        {
          Id = s.Id,
          ProjectName = s.ProjectName,
          ProjectDescription = s.ProjectDescription,
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<ProjectSessionModel> GetProjectKey(string key)
    {
      try
      {
        return GetNonDeletedAndActive<Project>(t => t.ProjectGuid
        .ToString() == key).Select(s => new ProjectSessionModel()
        {
          Id= s.Id,
          Name= s.ProjectName,
        }).FirstOrDefault();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<SelectListItem>> GetProjectSelect()
    {
      try
      {
        return GetNonDeletedAndActive<Project>(t=>true)
          .Select(s => new SelectListItem{
            Value=s.Id.ToString(),
            Text=s.ProjectName,
          }).ToList();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> UpdateProject(ProjectDTO model)
    {
      var project = await GetById<Project>(model.Id);
      project.ProjectDescription = model.ProjectDescription;
      project.ProjectName = model.ProjectName;
      Update(project);
      await unitOfWork.SaveChanges();
      return "1";
    }
  }
}
