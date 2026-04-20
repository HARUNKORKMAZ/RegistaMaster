using Microsoft.Identity.Client;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.UserModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class UserRepository : Repository, IUserRepository
  {
    private readonly RegistaMasterContext context;
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    public UserRepository(RegistaMasterContext _context, IUnitOfWork _unitOfWork, SessionModel _session) : base(_context, _session)
    {
      context = _context;
      unitOfWork = _unitOfWork;
      session = _session;
    }
    public async Task<string> AddUser(User model)
    {
      try
      {
        model.CustomerID = session.CustomerId;
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> ChangeAuthorization(UserDetailDTO model)
    {
      try
      {
        var user = await GetById<User>(model.ID);
        user.AuthorizationStatus = model.AuthorizationStatus;
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteUser(int Id)
    {
      try
      {
        var user = await GetById<User>(Id);
        user.ObjectStatus = ObjectStatus.Deleted;
        user.Status = Status.Passive;
        Update(user);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<UserCreatedByDTO>> GetCreatedBy()
    {
      try
      {
        var users = GetNonDeletedAndActive<User>(t => true).Select(s => new UserCreatedByDTO()
        {
          Id = s.Id,
          Name = s.Name,
          SurName = s.Surname,
        }).ToList();
        return users;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IQueryable<UserDTO>> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<User>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new UserDTO()
        {
          ID = s.Id,
          Name = s.Name,
          Surname = s.Surname,
          Email = s.Email,
          Username = s.Username,
          Password = s.Password,
          AuthorizationStatus = s.AuthorizationStatus,
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetResponsible()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> ResponsibleHelpers = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Users
          .Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.AuthorizationStatus != AuthorizationStatus.Admin);
        foreach (var item in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = item.Id,
            Name = item.Name + " " + item.Surname,
          };
          ResponsibleHelpers.Add(helper);
        }
        return ResponsibleHelpers;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> UpdateUser(UserDetailDTO model)
    {
      try
      {
        var user = await GetById<User>(model.ID);
        user.Name = model.Name;
        user.Surname = model.Surname;
        user.Username = model.Username;
        user.Email = model.Email;
        user.Password = model.Password;
        Update(user);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<UserDetailDTO> UserDetails(int id)
    {
      var user = await GetById<User>(id);
      return new UserDetailDTO()
      {
        ID = user.Id,
        Name = user.Name,
        Surname = user.Surname,
        Username = user.Username,
        Image = user.Image,
        Email = user.Email,
        Password = user.Password,
        AuthorizationStatus = user.AuthorizationStatus,
      };
    }

    public async Task<UserDTO> UserSessionDetail()
    {
      try
      {
        var model = await unitOfWork.Repository.GetById<User>(unitOfWork.GetSession().Id);
        var userDetail = new UserDTO()
        {
          Username = model.Username,
          Name = model.Name,
          Surname = model.Surname,
          Email = model.Email,
          Password = model.Password,
          AuthorizationStatus = model.AuthorizationStatus,
        };
        return userDetail;
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
