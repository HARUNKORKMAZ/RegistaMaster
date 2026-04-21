using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.VersionModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using Version = RegistaMaster.Domain.Entities.Version;

namespace RegistaMaster.WebApp.Controllers
{
  public class DefinationController : Controller
  {
    private readonly IUnitOfWork unitOfWork;

    public DefinationController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public async Task<object> GetModules(DataSourceLoadOptions options)
    {
      try
      {
        var model = await unitOfWork.ModuleRepository.GetModule();
        return DataSourceLoader.Load(model, options);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> AddMoules(string values)
    {
      try
      {
        var model = JsonConvert.DeserializeObject<Module>(values);
        await unitOfWork.ModuleRepository.CreateModule(model);
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IActionResult> ModuleUpdate(int key, string values)
    {
      try
      {
        var model = await unitOfWork.Repository.GetById<Module>(key);
        JsonConvert.PopulateObject(values, model);
        await unitOfWork.ModuleRepository.UpdateModule(model);
        await unitOfWork.SaveChanges();
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> DeleteModule(int key)
    {
      try
      {
        await unitOfWork.Repository.Delete<Module>(key);
        await unitOfWork.SaveChanges();
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetVersion(DataSourceLoadOptions options)
    {
      try
      {
        var model = await unitOfWork.VersionRepository.GetList();
        return DataSourceLoader.Load(model, options);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<IActionResult> AddVersion(string values)
    {
      try
      {
        var model = JsonConvert.DeserializeObject<VersionDTO>(values);
        await unitOfWork.VersionRepository.AddVersion(model);
        return Ok();
      }

      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<IActionResult> UpdateVersion(int key, string values)
    {
      try
      {
        var model = await unitOfWork.Repository.GetById<Version>(key);
        JsonConvert.PopulateObject(values, model);
        await unitOfWork.VersionRepository.UpdateVersion(model);
        await unitOfWork.SaveChanges();
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<object> DeleteVersion(int key)
    {
      try
      {
        await unitOfWork.Repository.Delete<Version>(key);
        await unitOfWork.SaveChanges();
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<IActionResult> GetDatabaseStatus()
    {
      try
      {
        var model = unitOfWork.Repository.GetEnumSelect<DatabaseChangeStatus>();
        var resultJson = JsonConvert.SerializeObject(model);
        return Content(resultJson, "application/json");
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    public async Task<object> GetProject(DataSourceLoadOptions options)
    {
      var model = await unitOfWork.ModuleRepository.GetProject();
      return DataSourceLoader.Load(model, options);
    }
  }
}
