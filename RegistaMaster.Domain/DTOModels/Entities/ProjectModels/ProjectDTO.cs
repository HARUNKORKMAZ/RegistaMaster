using Microsoft.AspNetCore.Mvc.Rendering;

namespace RegistaMaster.Domain.DTOModels.Entities.ProjectModels
{
  public class ProjectDTO
  {
    public int ID { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public int ProjectID { get; set; }
    public List<SelectListItem> Project { get; set; }
  }
}
