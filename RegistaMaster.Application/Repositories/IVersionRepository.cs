using RegistaMaster.Domain.DTOModels.Entities.VersionModels;

using Version = RegistaMaster.Domain.Entities.Version;

namespace RegistaMaster.Application.Repositories
{
  public interface IVersionRepository :IRepository
  {
    Task<IQueryable<VersionDTO>> GetList();
    Task<string> AddVersion(VersionDTO model);
    Task<string> UpdateVersion(Version model);
    Task<string> DeleteVersion(int Id);
    Task<string> DeleteVersionWithProjectId(int Id);
    double GetVersionName(int Id);
    Task<string> UpdateVersion(VersionDTO model);
    Task<string> GetVersion(int Id);


  }
}
