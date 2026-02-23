using RegistaMaster.Domain.DTOModels.ChartModels;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistaMaster.Application.Repositories
{
  public interface IHomeRepository : IRepository
  {
    Task<List<ActionDTO>> GetActionDTOHome();
    Task<ChartDTO> AdminChart();
    Task<List<UserChartDTO>> AdminChartUserActions();
    Task<ChartDTO> TeamLeaderChart(int Id);
    Task<UserChartDTO> DeveloperChart(int Id);

  }
}
