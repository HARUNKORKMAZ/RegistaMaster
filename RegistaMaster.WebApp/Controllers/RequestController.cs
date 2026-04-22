using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.RequestModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using Action = RegistaMaster.Domain.Entities.Action;

namespace RegistaMaster.WebApp.Controllers
{
  public class RequestController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IWebHostEnvironment environment;
    private readonly RegistaMasterContext context;
    public RequestController(IUnitOfWork _unitOfWork, IWebHostEnvironment _environment, RegistaMasterContext _context)
    {
      unitOfWork = _unitOfWork;
      this.environment = _environment;
      this.context = _context;
    }
    public async Task<IActionResult> Index()
    {
      var model = new RequestDTO
      {
        NotificationType = await unitOfWork.RequestRepository.NotificationTypeSelectList(),
        Category = await unitOfWork.RequestRepository.CategorySelectList(),
        Project = await unitOfWork.RequestRepository.GetProjectSelect(),
        Responsible = await unitOfWork.RequestRepository.ResponsibleSelecetList()
      };
      return View(model);
    }

    [HttpPost]
    public async Task<Request> Create(Request model, string base64)
    {
      try
      {
        if (base64 != null)
        {
          string webRootPath = environment.WebRootPath;
          var imageString = base64.Split(',');
          Guid guidFile = Guid.NewGuid();
          string fileName = "RequestImage" + guidFile + ".jpg";
          var path = Path.Combine(webRootPath, "\\Documents\\RequestDocs", fileName);
          var bytes = Convert.FromBase64String(imageString[1]);
          using (var imgFile = new FileStream(path, FileMode.Create))
          {
            imgFile.Write(bytes, 0, bytes.Length);
            imgFile.Flush();
          }


          var Extension = Path.GetExtension(fileName);
          model.PictureUrl = fileName;
          return await unitOfWork.RequestRepository.RequestAdd(model);
        }
        else
        {
          return await unitOfWork.RequestRepository.RequestAdd(model);
        }
      }
      catch (Exception e)
      {

        throw e;
      }

    }

    public Task<string> SaveRequestDoc(List<IFormFile> files, int ID)
    {
      try
      {
        return unitOfWork.RequestRepository.AddRequestFiles(files, ID);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    [HttpPost]
    public async Task<Request> RequestUpdate(RequestGridDTO model)
    {
      try
      {
        return await unitOfWork.RequestRepository.UpdateRequest(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      var models = await unitOfWork.RequestRepository.GetListWithFiles();
      return DataSourceLoader.Load(models, options);
    }

    public async Task<string> GetRequestDetail(int ID)
    {
      return JsonConvert.SerializeObject(unitOfWork.ActionRepository.GetActionsByRequestcId(ID));
    }

    [HttpPost]
    public async Task<string> RequestDelete(int ID)
    {
      try
      {
        return await unitOfWork.RequestRepository.RequestDelete(ID);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IActionResult> AddAction(Action model)
    {
      try
      {
        await unitOfWork.ActionRepository.AddAction(model);
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IActionResult> GetRequestStatus()
    {
      try
      {
        var models = unitOfWork.Repository.GetEnumSelect<RequestStatus>();
        var resultJson = JsonConvert.SerializeObject(models);
        return Content(resultJson, "application/json");
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetProject(DataSourceLoadOptions loadOptions)
    {
      try
      {
        var responsibleHelper = await unitOfWork.RequestRepository.GetProject();
        return DataSourceLoader.Load(responsibleHelper, loadOptions);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetModules(DataSourceLoadOptions loadOptions)
    {
      try
      {
        var model = await unitOfWork.RequestRepository.GetModeluSelect();
        return DataSourceLoader.Load(model, loadOptions);
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task<object> GetVersion(DataSourceLoadOptions loadOptions)
    {
      try
      {
        var model = await unitOfWork.RequestRepository.GetVersionSelect();
        return DataSourceLoader.Load(model, loadOptions);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<SelectListItem>> GetNotificationType()
    {
      return await unitOfWork.RequestRepository.NotificationTypeSelectList();
    }

    public async Task<string> GetModuleList(int Id)
    {
      try
      {
        var model = await unitOfWork.RequestRepository.GetModuleList(Id);
        return JsonConvert.SerializeObject(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> GetVersionList(int ID)
    {
      try
      {
        var model = await unitOfWork.RequestRepository.GetVersionList(ID);
        if (model.Count == 0)
          return "1";
        return JsonConvert.SerializeObject(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> CheckActionsForDeleteRequest(int ID)
    {
      if (unitOfWork.Repository.GetQueryable<Action>(t => t.RequestID == ID && t.ObjectStatus == ObjectStatus.NonDeleted).Any())
        return "2";
      return "1";
    }
    [HttpPost]
    public async Task<string> RequestDeleteWithAction(int ID)
    {
      try
      {
        return await unitOfWork.RequestRepository.RequestDeleteWithActions(ID);
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> CompleteRequest(int ID)
    {
      try
      {
        return await unitOfWork.RequestRepository.CompleteRequest(ID);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteFile([FromBody] List<string> files)
    {
      try
      {
        return await unitOfWork.RequestRepository.DeleteRequestFiles(files);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

  }
}
