using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RegistaMaster.Domain.DTOModels.Entities.UserLogModel
{
  public class UserLogDTO
  {
    public string ProjectKey { get; set; }
    public string NameSurname{ get; set; }
    public DateTime LoginDate { get; set; }
    public int? ClientId { get; set; }
    public int? MemberId { get; set; }
  }
}
