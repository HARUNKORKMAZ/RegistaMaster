using Microsoft.EntityFrameworkCore;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class SecurityRepository : ISecurityRepository
  {
    private readonly RegistaMasterContext context;
    public SecurityRepository(RegistaMasterContext _context)
    {
      context = _context;
    }
    public async Task<User> Login(string username , string password)
    {
      return await context.Users.FirstOrDefaultAsync(t=>(t.UserName==username || t.Email == username) && t.Password==password && t.ObjectStatus == ObjectStatus.NonDeleted && t.Status ==Status.Active);
    }
  }
}
