using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.UserModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using System.Diagnostics.Contracts;
using System.Runtime.Loader;

namespace RegistaMaster.WebApp.Controllers
{
  public class UserController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    public UserController(IUnitOfWork _unitOfWork, SessionModel _session)
    {
      unitOfWork = _unitOfWork;
      session = unitOfWork.GetSession();
    }

    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      var models = await unitOfWork.UserRepository.GetList();
      return DataSourceLoader.Load(models, options);
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> UserDetail()
    {
      try
      {
        return View(await unitOfWork.UserRepository.UserSessionDetail());
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task<string> FileUpload(IFormFile fileUrl)
    {
      try
      {
        string fileName = "";
        if (fileUrl != null)
        {
          string extension = Path.GetExtension(fileUrl.FileName);
          Guid guid = Guid.NewGuid();
          fileName = "user_" + guid.ToString() + extension;
          var path = Path.Combine("wwwroot/Modernize/Img/ProfilePhotos", fileName);

          using (var stream = new FileStream(path, FileMode.Create))
          {
            fileUrl.CopyTo(stream);
          }
        }
        return fileName;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IActionResult> GetAuthStatus()
    {
      try
      {
        var models = unitOfWork.Repository.GetEnumSelect<AuthorizationStatus>();
        var resultJson = JsonConvert.SerializeObject(models);
        return Content(resultJson, "application/json");
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    [HttpPost]
    public async Task<string> AddUser(User model)
    {
      try
      {
        await unitOfWork.UserRepository.AddUser(model);
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> UpdateUser(UserDetailDTO model)
    {

      try
      {
        return await unitOfWork.UserRepository.UpdateUser(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> ChangeAuthorization(UserDetailDTO model)
    {
      try
      {
        return await unitOfWork.UserRepository.ChangeAuthorization(model);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<string> DeleteUser(int Id)
    {
      try
      {
        await unitOfWork.UserRepository.DeleteUser(Id);
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
