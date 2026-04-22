using System;
using System.Collections.Generic;
using System.Text;

namespace RegistaMaster.Domain.DTOModels.Entities.CustomerModels
{
  public class CustomerDTO
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
  }
}
