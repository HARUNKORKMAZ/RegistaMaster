using RegistaMaster.Domain.Enums;

namespace RegistaMaster.Domain.DTOModels.Entities.UserModels
{
  public class UserDetailDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string UserName { get; set; }
    public string Image { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public AuthorizationStatus AuthorizationStatus { get; set; }

  }
}
