using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegistaMaster.Application.Features.Auth;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Enums;
using RegistaMaster.WebApp.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace RegistaMaster.WebApp.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger logger;
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    public HomeController(ILogger<HomeController> _logger, IUnitOfWork _unitOfWork)
    {
      logger = _logger;
      unitOfWork = _unitOfWork;
      session = unitOfWork.GetSession();
    }
    [Auth]
    public async Task<IActionResult> Index()
    {
      switch (session.Authorization)
      {
        case AuthorizationStatus.Admin:
          ViewBag.Chart = await unitOfWork.HomeRepository.AdminChart();
          break;
        case AuthorizationStatus.TeamLeader:
          ViewBag.Chart = await unitOfWork.HomeRepository.TeamLeaderChart(session.ID);
          break;
        case AuthorizationStatus.Developer:
          ViewBag.Chart = await unitOfWork.HomeRepository.DeveloperChart(session.ID);
          break;
      }
      ViewBag.Responsible = await unitOfWork.RequestRepository.ResponsibleSelecetList();
      return View();
    }

    public async Task<string> GetDashboard()
    {
      try
      {
        return JsonConvert.SerializeObject(await unitOfWork.HomeRepository.AdminChartUserActions());
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> GetActionHome(DataSourceLoadOptions options)
    {
      var model = await unitOfWork.HomeRepository.GetActionDTOHome();
      return DataSourceLoader.Load(model, options);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
