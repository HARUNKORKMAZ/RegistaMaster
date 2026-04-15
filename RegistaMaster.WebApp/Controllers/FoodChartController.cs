using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;

namespace RegistaMaster.WebApp.Controllers
{
  public class FoodChartController : Controller
  {
    private readonly IUnitOfWork unitOfWork;

    public FoodChartController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }

    public IActionResult Index()
    {
      return View();
    }
    public async Task<object> GetList(DataSourceLoadOption option)
    {
      var models = unitOfWork.FoodChartRepository.GetList();
      return DataSourceLoader.Load(models, option);
    }

    public async Task<IActionResult> FoodChartAdd(string values)
    {
      try
      {
        var result = await unitOfWork.FoodChartRepository.AddFoodChart(values);
        return result =="1" ? Ok() : BadRequest(result);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> FoodChartEdit(int key,string values)
    {
      try
      {
        return await unitOfWork.FoodChartRepository.UpdateFoodChart(key,values);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> FoodChartDelete(int key)

    {
      try
      {
        return await unitOfWork.FoodChartRepository.DeleteFoodChart(key);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<object> FoodChartGraph()
    {
      return View();
    }

    [HttpPost]
    public async Task<object> FoodChartGraph(int year)
    {
      try
      {
        return JsonConvert.SerializeObject(unitOfWork.FoodChartRepository.GetChart(year));
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    [HttpPost]
    public async Task<IActionResult> UploadExcel(IFormFile file)
    {
      try
      {
        var result = await unitOfWork.FoodChartRepository.UplaodExcel(file);
        return result == "1" ?  RedirectToAction(nameof(Index)) : BadRequest(result);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
