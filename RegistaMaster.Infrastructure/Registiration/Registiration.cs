using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Registiration
{
  public  static class Registiration
  {
    public static void AddDatabase(this IServiceCollection services,string connectionString)
    {
      services.AddDbContext<RegistaMasterContext>(options =>
      options.UseSqlServer(connectionString));
    }
  }
}
