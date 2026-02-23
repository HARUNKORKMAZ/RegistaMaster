using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.WebApp.Controllers
{
  public class CustomerController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    public CustomerController(IUnitOfWork _unitOfWork)
    {
      unitOfWork = _unitOfWork;
    }
    public IActionResult Index()
    {
      return View();
    }

    public async Task<object> GetList(DataSourceLoadOptions options)
    {
      var models = await unitOfWork.CustomerRepository.GetList();
      return DataSourceLoader.Load(models, options);
    }

    public async Task<IActionResult> CustomerAdd(string values)
    {
      try
      {
        var model = JsonConvert.DeserializeObject<Customer>(values);
        await unitOfWork.CustomerRepository.CustomerAdd(model);
        return Ok();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    [HttpPut]
    public async Task<string> CustomerEdit(int Key, string values)
    {
      try
      {
        var customer = await unitOfWork.Repository.GetById<Customer>(Key);
        JsonConvert.PopulateObject(values, customer);
        unitOfWork.CustomerRepository.Update(customer);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteCustomer(int Key)
    {
      try
      {
        await unitOfWork.Repository.Delete<Customer>(key);
        await unitOfWork.SaveChanges();
        return "1";

      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
