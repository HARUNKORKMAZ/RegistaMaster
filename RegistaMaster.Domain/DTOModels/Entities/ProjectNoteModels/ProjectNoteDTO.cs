namespace RegistaMaster.Domain.DTOModels.Entities.ProjectNoteModels
{
  public class ProjectNoteDTO
  {
    public int ID { get; set; }
    public DateTime? Date { get; set; }
    public string? NoteType { get; set; }
    public string? Description { get; set; }
    public int CreatedBy { get; set; }
  }
}
