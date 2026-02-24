using RegistaMaster.Domain.Enums;

namespace RegistaMaster.Domain.DTOModels.Entities.UserModels
{
  public class UserDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public AuthorizationStatus AuthorizatinStatus { get; set; }
  }
}
