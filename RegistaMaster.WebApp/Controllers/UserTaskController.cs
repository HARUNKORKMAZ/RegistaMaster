using Microsoft.AspNetCore.Mvc;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.WebApp.Controllers
{
  public class UserTaskController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    public UserTaskController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }
    public IActionResult Index()
    {
      return View();
    }
    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      try
      {
        var model = await unitOfWork.UserTaskRepository.GetList();
        return DataSourceLoader.Load(model, options);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    public async Task<string> AddUserTask(UserTask model)
    {
      try
      {
        return await unitOfWork.UserTaskRepository.AddUserTask(model);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    public async Task<string> EditUserTask(UserTask model)
    {
      try
      {
        return await unitOfWork.UserTaskRepository.UpdateUserTask(model);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    public async Task<string> DeleteUserTask(int id)
    {
      try
      {
        return await unitOfWork.UserTaskRepository.DeleteUserTask(id);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}
