using Microsoft.AspNetCore.Http;
using RegistaMaster.Domain.DTOModels.Entities.ChartsModel;
using RegistaMaster.Domain.DTOModels.Entities.FoodChartsModels;

namespace RegistaMaster.Application.Repositories
{
  public interface IFoodChartRepository:IRepository
  {
    IQueryable<FoodChartsDTO> GetList();
    Task<string> AddFoodChart(string values);
    Task<string> UpdateFoodChart(int key,string value);
    Task<string> DeleteFoodChart(int id);
    Task<List<MonthDTO>> GetChart(int year);
    Task<string> AddFoodChartFromExcel(Stream fileStream);
    Task<int> CheckRecordForDate(DateTime date);
    Task<string> UplaodExcel(IFormFile file);
  }
}
