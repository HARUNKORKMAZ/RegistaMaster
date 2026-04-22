using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ChartsModel;
using RegistaMaster.Domain.DTOModels.Entities.FoodChartsModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class FoodChartsRepository : Repository, IFoodChartRepository
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IUnitOfWork unitOfWork;
    public FoodChartsRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      session = _session;
      unitOfWork = _unitOfWork;
    }

    public async Task<string> AddFoodChart(string values)
    {
      try
      {
        var model = JsonConvert.DeserializeObject<FoodChart>(values);
        int recordCount = await unitOfWork.FoodChartRepository.CheckRecordForDate(model.Date);
        if (recordCount > 0)
          return "Girilen Tarih İçin Kayıt Bulunmaktadır.";

        await Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task<string> AddFoodChartFromExcel(Stream fileStream)
    {
      try
      {
        ExcelPackage package = new ExcelPackage(fileStream);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

        int rowCount = worksheet.Dimension.Rows;
        int columnCount = worksheet.Dimension.Columns;
        for (int row = 2; row <= rowCount; row++)
        {
          var foodChart = new FoodChart();
          for (int col = 1; col <= columnCount; col++)
          {
            var cellValue = worksheet.Cells[row, col].Value;
            if (cellValue != null)
            {
              if (DateTime.TryParse(cellValue.ToString(), out DateTime date))
                foodChart.Date = date;
              else if (int.TryParse(cellValue.ToString(), out int personNumber))
                foodChart.PersonNumber = personNumber;
            }
          }
          if (foodChart.Date != default || foodChart.PersonNumber != default)
          {
            string foodChartJson = JsonConvert.SerializeObject(foodChart);
            await AddFoodChart(foodChartJson);
          }
        }
        return "1";

      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<int> CheckRecordForDate(DateTime date)
    {
      try
      {
        return await unitOfWork.Repository.GetNonDeletedAndActive<FoodChart>(t =>
        t.Date.Year == date.Year &&
        t.Date.Month == date.Month &&
        t.Date.Day == date.Day).CountAsync();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteFoodChart(int id)
    {
      try
      {
        await Delete<FoodChart>(id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<MonthDTO>> GetChart(int year)
    {
      var chart = new FoodChartDTO();
      var food = GetNonDeletedAndActive<FoodChart>(t => t.Date.Year == year);
      for (int month = 1; month <= 12; month++)
      {
        var monthFood = food.Where(t => t.Date.Month == month).ToList();
        foreach (var item in monthFood)
        {
          switch (month)
          {
            case 1: chart.January += item.PersonNumber; break;
            case 2: chart.February += item.PersonNumber; break;
            case 3: chart.March += item.PersonNumber; break;
            case 4: chart.April += item.PersonNumber; break;
            case 5: chart.May += item.PersonNumber; break;
            case 6: chart.June += item.PersonNumber; break;
            case 7: chart.July += item.PersonNumber; break;
            case 8: chart.August += item.PersonNumber; break;
            case 9: chart.September += item.PersonNumber; break;
            case 10: chart.October += item.PersonNumber; break;
            case 11: chart.November += item.PersonNumber; break;
            case 12: chart.December += item.PersonNumber; break;
          }
        }
      }
      List<MonthDTO> monthDTOs = new List<MonthDTO>();
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Ocak",
        Count = chart.January
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Şubat",
        Count = chart.February
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Mart",
        Count = chart.March
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Nisan",
        Count = chart.April
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Mayıs",
        Count = chart.May
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Haziran",
        Count = chart.June
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Temmuz",
        Count = chart.July
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Ağustos",
        Count = chart.August
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Eylül",
        Count = chart.September
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Ekim",
        Count = chart.October
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Kasım",
        Count = chart.November
      });
      monthDTOs.Add(new MonthDTO()
      {
        MonthName = "Aralık",
        Count = chart.December
      });
      return monthDTOs;
    }

    public IQueryable<FoodChartsDTO> GetList()
    {
      try
      {
        return GetQueryable<FoodChart>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new FoodChartsDTO
        {
          ID = s.ID,
          Date = s.Date,
          PersonNumber = s.PersonNumber,
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> UpdateFoodChart(int key, string value)
    {
      try
      {
        var model = await GetById<FoodChart>(key);
        JsonConvert.PopulateObject(value, model);
        Update(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> UplaodExcel(IFormFile file)
    {
      try
      {
        if (file == null || file.Length <= 0)
        {
          return "Excel Dosyası Yüklenemedi.";
        }
        using (var stream = new MemoryStream())
        {
          await file.CopyToAsync(stream);
          stream.Position = 0;
          await AddFoodChartFromExcel(stream);
        }
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }
  }
}
