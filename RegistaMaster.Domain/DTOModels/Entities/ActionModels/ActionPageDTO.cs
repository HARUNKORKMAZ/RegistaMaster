using RegistaMaster.Domain.Enums;

namespace RegistaMaster.Domain.DTOModels.Entities.ActionModels
{
  public class ActionPageDTO
  {
    public int Id { get; set; }
    public string Responsible { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public string Description { get; set; }
    public ActionStatus ActionStatus { get; set; }
    public int RequestId { get; set; }
  }
}
