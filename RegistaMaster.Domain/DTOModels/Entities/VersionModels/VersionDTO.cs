namespace RegistaMaster.Domain.DTOModels.Entities.VersionModels
{
  public class VersionDTO
  {
    public int  Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public  DateTime Date { get; set; }
    public bool DatabaseChange { get; set; }
    public bool IsNewVersion { get; set; }
    public int ProjectId { get; set; }
  }
}
