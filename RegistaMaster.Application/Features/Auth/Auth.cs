using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using System.Text;
using System.Text.Json;

namespace RegistaMaster.Application.Features.Auth
{
  public class Auth : ResultFilterAttribute, IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var session = context.HttpContext.Session;
      if (session == null)
      {
        var url = context.HttpContext.Request.Path;
        context.Result = new RedirectToActionResult("Login", "Security", new { url });
      }
      else
      {
        Byte[] ss;
        var ctry = session.TryGetValue("Login", out ss);
        if (!ctry)
        {
          var url = context.HttpContext.Request.Path;
          context.Result = new RedirectToActionResult("Login", "Security", new { url });
          return;
        }
        var sessionModel = FromByteArray<SessionModel>(ss);
        if (sessionModel == null)
        {
          var url = context.HttpContext.Request.Path;
          context.Result = new RedirectToActionResult("Login", "Security", new { url });
          return;
        }
      }
    }
    public T FromByteArray<T>(byte[] data)
    {
      if (data == null)
        return default(T);
      var stringObj=Encoding.ASCII.GetString(data);
      return JsonSerializer.Deserialize<T>(stringObj);
    }
  }
}
