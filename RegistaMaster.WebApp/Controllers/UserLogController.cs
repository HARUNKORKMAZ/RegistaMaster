using Microsoft.AspNetCore.Mvc;
using RegistaMaster.Application.Repositories;

namespace RegistaMaster.WebApp.Controllers
{
  public class UserLogController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    public UserLogController(IUnitOfWork _unitOfWork)
    {
        unitOfWork = _unitOfWork;
    }
    public IActionResult Index()
    {
      return View();
    }
    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      var model = unitOfWork.UserLogRepository.GetList();
      return DataSourceLoader.Load(model, options);
    }
  }
}
