using RegistaMaster.Domain.Entities;
using System.ComponentModel;

namespace RegistaMaster.Domain.DTOModels.Entities.ErrorLogModels
{
  public class ErrorLogDTO
  {
    public string ProjectKey { get; set; }
    public string NameSurname { get; set; }
    public DateTime ErrorDate{ get; set; }
    public string ErrorDesc { get; set; }
    public int  ClientId { get; set; }
    public int  MemberId { get; set; }
  }
}
