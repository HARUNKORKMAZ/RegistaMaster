using Microsoft.AspNetCore.Mvc.Rendering;

namespace RegistaMaster.Domain.DTOModels.Entities.ProjectModels
{
  public class ProjectDTO
  {
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public int ProjectId { get; set; }
    public List<SelectListItem> Project { get; set; }
  }
}
