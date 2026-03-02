using RegistaMaster.Domain.DTOModels.Entities.ModuleModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.Entities;

namespace RegistaMaster.Application.Repositories
{
  public interface IModuleRepository : IRepository
  {
    Task<IQueryable<ModuleDTO>> GetModule();
    Task<string> GetModules(int id);
    Task<string> CreateModule(Module model);
    Task<string> UpdateModule(Module model);
    Task<string> DeleteModule(int id);
    Task<List<ResponsibleDevextremeSelectListHelper>> GetProject();
    Task<string> DeleteModuleWithProjectId(int id);
    Task<string> UpdateModule(ModuleDTO model);
  }
}
