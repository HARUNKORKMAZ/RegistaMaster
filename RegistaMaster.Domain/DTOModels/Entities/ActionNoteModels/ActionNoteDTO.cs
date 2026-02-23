namespace RegistaMaster.Domain.DTOModels.Entities.ActionNoteModels
{
  public class ActionNoteDTO
  {
    public int Id { get; set; }
    public int ActionId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
  }
}
