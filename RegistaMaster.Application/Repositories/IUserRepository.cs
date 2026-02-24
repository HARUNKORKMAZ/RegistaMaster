using RegistaMaster.Domain.DTOModels.Entities.UserModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IUserRepository : IRepository
  {
    Task<string> AddUser(User model);
    Task<IQueryable<UserDTO>> GetList();
    Task<List<ResponsibleDevextremeSelectListHelper>> GetResponsible();
    Task<UserDetailDTO> UserDetails(int id);
    Task<string> UpdateUser(UserDetailDTO model);
    Task<string> DeleteUser(int Id);
    Task<string> ChangeAuthorization(UserDetailDTO model);
    Task<List<UserCreatedByDTO>> GetCreatedBy();
    Task<UserDTO> UserSessionDetail();

  }
}
