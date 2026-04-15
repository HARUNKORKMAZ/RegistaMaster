using Microsoft.AspNetCore.Mvc;
using RegistaMaster.Application.Repositories;

namespace RegistaMaster.WebApp.Controllers
{
  public class ErrorLogController : Controller
  {
    private readonly IUnitOfWork unitOfWork;

    public ErrorLogController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }

    public IActionResult Index()
    {
      return View();
    }
    public async Task<object> GetList(DataSourceLoadOption option)
    {
      var model = unitOfWork.ErrorLogRepository.GetList();
      return DataSourceLoader.Load(model, option);
    }
  }
}
