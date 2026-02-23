using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistaMaster.Application.Services.SecurityService
{
  public interface ISessionService
  {
    void SetSession<T>(string key, T model);
    T GetSession<T>(string key);
    SessionModel GetUser();
    void SetUser(User user);
    SessionModel GetInjection();
    void CleanSession();
    ProjectSessionModel GetProject();
  }
}
