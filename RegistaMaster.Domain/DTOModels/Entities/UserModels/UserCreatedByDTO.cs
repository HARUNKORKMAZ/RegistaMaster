namespace RegistaMaster.Domain.DTOModels.Entities.UserModels
{
  public class UserCreatedByDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string FullName => Name + " " + SurName;
  }
}
