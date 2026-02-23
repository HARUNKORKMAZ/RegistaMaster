using Microsoft.AspNetCore.Mvc;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Application.Services.SecurityService;
using RegistaMaster.Domain.DTOModels.SecurityModels;

namespace RegistaMaster.WebApp.Controllers
{
  public class SecurityController : Controller
  {
    private readonly ISecurityRepository securityRepository;
    private readonly ISessionService sessionService;
    public SecurityController(ISecurityRepository _securityRepository, ISessionService _sessionService)
    {
      sessionService = _sessionService;
      securityRepository = _securityRepository;
    }
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public IActionResult Login(PathString url)
    {
      var model = new LoginModel { Url = url };
      var user = sessionService.GetUser();
      return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
      var user = await securityRepository.Login(model.UserName, model.Password);
      if (user != null)
      {
        sessionService.SetUser(user);
        if (model.Url != null && model.Url != "/")
          return Redirect(model.Url);
        else
          return Redirect("/Home/Index");
      }
      else
      {
        ModelState.AddModelError("All", "Kullanıcı adı veya şifre hatalı.");
        return View(model);
      }
    }
    public IActionResult Logout()
    {
      sessionService.CleanSession();
      return RedirectToAction("/Login");
    }
  }
}
