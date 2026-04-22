using Microsoft.AspNetCore.Http;
using RegistaMaster.Application.Services.SecurityService;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Domain.Exceptions;
using RegistaMaster.Persistance.RegistaMasterContextes;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RegistaMaster.Infrastructure.Services.SecurityServices
{
  public class SessionService : ISessionService
  {
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly RegistaMasterContext context;

    public SessionService(IHttpContextAccessor _httpContextAccessor, RegistaMasterContext _context)
    {
      httpContextAccessor = _httpContextAccessor;
      context = _context;
    }

    public void CleanSession()
    {
      httpContextAccessor.HttpContext.Session.Clear();
    }

    public SessionModel GetInjection()
    {
      var user = new SessionModel();
      user.ID = -1;
      if (httpContextAccessor.HttpContext == null)
      {
        user.ID = -1;
        return user;
      }

      var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      if (val != null)
      {
        var val2 = httpContextAccessor.HttpContext.User.FindFirst("CustomerId");
        if (val2 != null)
          user.CustomerID = Convert.ToInt32(val2.Value);
        if (val != null)
          user.CustomerID = Convert.ToInt32(val.Value);
      }
      else
      {
        var usr = GetUser();
        if (usr != null)
          user = usr;
      }
      return user;

    }

    public ProjectSessionModel GetProject()
    {
      try
      {
        var key = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        return context.Projects.Where(t => t.ProjectGuid.ToString() == key && t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active).Select(s => new ProjectSessionModel()
        {
          ID = s.ID,
          Name = s.ProjectName
        }).FirstOrDefault();

      }
      catch (Exception)
      {

        throw new UnAuth("Giriş Yapılamadı");
      }
    }

    public T GetSession<T>(string key)
    {
      try
      {
        var session = httpContextAccessor.HttpContext.Session;
        byte[] ss;
        var ctry = session.TryGetValue(key, out ss);
        if (!ctry)
          return default(T);
        if (ss == null)
          return default(T);
        return FromByteArray<T>(ss);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public byte[] TobyteArray<T>(T obj)
    {
      var objToString = JsonSerializer.Serialize(obj);
      return Encoding.ASCII.GetBytes(objToString);
    }
    public T FromByteArray<T>(byte[] obj)
    {
      if (obj == null)
        return default;
      var objToString = Encoding.ASCII.GetString(obj);
      return JsonSerializer.Deserialize<T>(objToString);
    }

    public SessionModel GetUser()
    {
      return GetSession<SessionModel>("Login");
    }

    public void SetSession<T>(string key, T model)
    {
      try
      {
        var ss = TobyteArray<T>(model);
        httpContextAccessor.HttpContext.Session.Set(key, ss);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public void SetUser(User user)
    {
      try
      {
        var sesssionmodel= new SessionModel()
        {
          CustomerID=user.CustomerID,
          ID=user.ID,
          Name=user.Name,
          Surname=user.Surname,
          Authorization=user.AuthorizationStatus,
        };
        SetSession("Login", sesssionmodel);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
