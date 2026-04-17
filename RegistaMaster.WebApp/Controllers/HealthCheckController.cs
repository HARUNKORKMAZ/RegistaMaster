using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using RegistaMaster.Application.Repositories;

namespace RegistaMaster.WebApp.Controllers
{
  public class HealthCheckController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    public HealthCheckController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }
    public IActionResult Index()
    {
      return View();
    }
    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      var model = unitOfWork.HealthCheckRepository.GetList();
      return DataSourceLoader.Load(model, options);
    }
  }
}
