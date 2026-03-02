using Microsoft.AspNetCore.Mvc;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ModuleModels;
using RegistaMaster.Domain.DTOModels.Entities.ProjectModels;
using RegistaMaster.Domain.DTOModels.Entities.VersionModels;
using RegistaMaster.Domain.Entities;
using SixLabors.ImageSharp.Metadata;

namespace RegistaMaster.WebApp.Controllers
{
  public class ProjectController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    public ProjectController( IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }

    //PROJE
    public async Task<object> GetList(DataSourceLoadOption options)
    {
      var models = await unitOfWork.ProjectRepository.GetList();
      return DataSourceLoader.Load(models, options);
    }
    public async Task<string> AddProject(Project model)
    {
      try
      {
        return await unitOfWork.ProjectRepository.AddProject(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> ProjectEdit(ProjectDTO model)
    {
      try
      {
        return await unitOfWork.ProjectRepository.UpdateProject(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> DeleteProject(int Id)
    {
      try
      {
        return await unitOfWork.ProjectRepository.DeleteProject(Id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<IActionResult> Index()
    {
      ViewBag.CreatedBy= unitOfWork.ProjectNoteRepository.CreatedBySelectList();
      var model = new ProjectDTO();
      return View(model);
    }


    // PROJE NOTU İŞLEMLERİ

    public async Task<string> GetProjectNotes(int id)
    {
      try
      {
        return await unitOfWork.ProjectNoteRepository.GetProjectNotes(id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> AddProjectNote(ProjectNote model)
    {
      try
      {
        return await unitOfWork.ProjectNoteRepository.ProjectNoteAdd(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> EditProjectNote(ProjectNoteDTO model)
    {
      try
      {
        return await unitOfWork.ProjectNoteRepository.UpdateProjectNote(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> DeleteProjectNote(int id)
    {
      try
      {
        await unitOfWork.Repository.Delete<ProjectNote>(id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> CheckRequest(int id)
    {
      try
      {
        return await unitOfWork.ProjectNoteRepository.CheckRequest(id);
      }
      catch (Exception)
      {

        throw;
      }
    }
    public async Task<object> GetCreatedBy(DataSourceLoadOptions loadOptions)
    {
      try
      {
        var model = await unitOfWork.UserRepository.GetCreatedBy();
        return DataSourceLoader.Load(model,loadOptions);
      }
      catch (Exception e)
      {

        throw e;
      }
    }


    // MODULE İŞLEMLERİ

    public async Task<string> AddModule(Module model)
    {
      try
      {
        return await unitOfWork.ModuleRepository.CreateModule(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> GetModules(int id)
    {
      try
      {
        return await unitOfWork.ModuleRepository.GetModule(id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> EditModule(ModuleDTO model)
    {
      try
      {
        return await unitOfWork.ModuleRepository.UpdateModule(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> DeleteModule(int id)
    {
      try
      {
        return await unitOfWork.ModuleRepository.DeleteModule(id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }


    //VERSİON İŞLEMLERİ

    public async Task<string> AddVersion(VersionDTO model)
    {
      try
      {
        return await unitOfWork.VersionRepository.AddVersion(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> GetVersion(int id)
    {
      try
      {
        return await unitOfWork.VersionRepository.GetVersion(id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> EditVersion(VersionDTO model)
    {
      try
      {
        return await unitOfWork.VersionRepository.UpdateVersion(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> DeleteVersion(int id)
    {
      try
      {
        return await unitOfWork.VersionRepository.DeleteVersion(id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

  }
}
