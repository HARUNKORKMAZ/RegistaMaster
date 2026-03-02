using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ModuleModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class ModuleRepository : Repository, IModuleRepository
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    public ModuleRepository(IUnitOfWork _unitOfWork, RegistaMasterContext _context, SessionModel _session) : base(_context, _session)
    {
      unitOfWork = _unitOfWork;
      context = _context;
      session = _session;
    }

    public async Task<string> CreateModule(Module model)
    {
      try
      {
        await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteModule(int id)
    {
      try
      {
        await Delete<Module>(id);
        await unitOfWork.SaveChanges();
        return "1";

      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteModuleWithProjectId(int id)
    {
      try
      {
        var module = GetNonDeletedAndActive<Module>(t => t.ProjectId == id);
        await DeleteRange(module.ToList());
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IQueryable<ModuleDTO>> GetModule()
    {
      try
      {
        return GetNonDeletedAndActive<Module>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new ModuleDTO()
        {
          Id = s.Id,
          Name = s.Name,
          Description = s.Description,
          ProjectID = s.ProjectId
        });
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> GetModules(int id)
    {
      try
      {
        var module = GetNonDeletedAndActive<Module>(t => t.ProjectId == id).Select(p => new ModuleDTO
        {
          Id = p.Id,
          Name = p.Name,
          Description = p.Description,
          ProjectID = p.ProjectId
        });
        return JsonConvert.SerializeObject(module);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetProject()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> responsibleHelper = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Projects
          .Where(t => t.Status == Status.Active && t.ObjectStatus == ObjectStatus.NonDeleted);
        foreach (var project in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = project.Id,
            Name = project.ProjectName
          };
          responsibleHelper.Add(helper);
        }
        return responsibleHelper;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> UpdateModule(Module model)
    {
      try
      {
        Update(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> UpdateModule(ModuleDTO model)
    {
      try
      {
        var module = await GetById<Module>(model.Id);
        module.Name = model.Name;
        module.Description = model.Description;
        module.ProjectId = model.ProjectID;
        Update(module);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }

    }
  }
}
