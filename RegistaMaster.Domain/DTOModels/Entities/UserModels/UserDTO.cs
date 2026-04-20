using RegistaMaster.Domain.Enums;

namespace RegistaMaster.Domain.DTOModels.Entities.UserModels
{
  public class UserDTO
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public AuthorizationStatus AuthorizationStatus { get; set; }
  }
}
