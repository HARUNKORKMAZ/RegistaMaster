using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using RegistaMaster.Domain.DTOModels.Entities.ActionNoteModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;

namespace RegistaMaster.WebApp.Controllers
{
  public class ActionController : Controller
  {
    private readonly IUnitOfWork unitOfWork;

    public ActionController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }
    public async Task<IActionResult> Index()
    {
      ViewBag.Responsible = await unitOfWork.RequestRepository.ResponsibleSelectList();
      return View();
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
      var model = new ActionDTO();
      model.ResponsibleHelperModelList = await unitOfWork.RequestRepository.ResponsibleSelectList();
      model.OpeningDate = DateTime.Now;
      model.EndDate = DateTime.Now;
      return View(model);
    }
    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      try
      {
        var model = unitOfWork.ActionRepository.GetList().OrderByDescending(t => t.Id);
        return DataSourceLoader.load(model, options);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IActionResult> GetActionStatus()
    {
      try
      {
        var models = unitOfWork.Repository.GetEnumSelect<ActionStatus>();
        var resultJson = JsonConvert.SerializeObject(models);
        return Content(resultJson, "application/json");
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task<IActionResult> GetPriorityActionStatus()
    {
      try
      {
        var models = unitOfWork.Repository.GetEnumSelect<ActionPriorityStatus>();
        var resultJson = JsonConvert.SerializeObject(models);
        return Content(resultJson, "application/json");
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetResponsible(DataSourceLoadOption loadOption)
    {
      try
      {
        var model = await unitOfWork.UserRepository.GetResponsible();
        return DataSourceLoader.Load(model, loadOption);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetCreateBy(DataSourceLoadOption loadOption)
    {
      try
      {
        var model = await unitOfWork.UserRepository.GetCreatedBy();
        return DataSourceLoader.Load(model, loadOption);
      }
      catch (Exception)
      {

        throw;
      }
    }
    public async Task<object> GetRequest(DataSourceLoadOption loadOption)
    {
      try
      {
        var model = await unitOfWork.ActionRepository.GetRequest();
        return DataSourceLoader.Load(model, loadOption);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    [HttpPost]
    public async Task<string> ActionUpdate(ActionDTO model)
    {
      try
      {
        return await unitOfWork.ActionRepository.ActionUpdate();
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    [HttpPost]
    public async Task<string> ActionDelete(int Id)
    {
      try
      {
        return await unitOfWork.ActionRepository.ActionDelete(Id);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    [HttpPost]
    public async Task<string> ChangeActionStatus(ActionDTO model)
    {
      try
      {
        return await unitOfWork.ActionRepository.ChangeActionStatus(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> AddActionNote([FromBody] ActionNote model)
    {
      try
      {
        await unitOfWork.ActionNoteRepository.AddActionNote(model);
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<object> GetActionNoteList(DataSourceLoadOption loadOption, int Id)
    {
      try
      {
        var model = unitOfWork.ActionNoteRepository.GetList(Id).OrderByDescending(t => t.Id).ToList();
        return DataSourceLoader.Load(model, loadOption);

      }
      catch (Exception e)
      {
        throw e;
      }
    }

    [HttpPost]
    public async Task<string> ActionNoteDelete(int Id)
    {
      try
      {
        await unitOfWork.Repository.Delete<ActionNote>(Id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    [HttpPost]
    public async Task<string> ActionNoteUpdate([FromBody] ActionNoteDTO model)
    {
      try
      {
        return await unitOfWork.ActionRepository.ActionNoteUpdate(model);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}
