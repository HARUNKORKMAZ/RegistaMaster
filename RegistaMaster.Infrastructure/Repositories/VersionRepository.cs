using Newtonsoft.Json;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.VersionModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using Version = RegistaMaster.Domain.Entities.Version;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class VersionRepository : Repository, IVersionRepository
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    private readonly RegistaMasterContext context;
    public VersionRepository(IUnitOfWork _unitOfWork, SessionModel _session, RegistaMasterContext _context) : base(_context, _session)
    {
      unitOfWork = _unitOfWork;
      session = _session;
      context = _context;
    }
    public async Task<string> AddVersion(VersionDTO model)
    {
      try
      {
        var olderVersion = GetVersionName(model.ProjectId);
        if (olderVersion != 0)
        {
          if (model.IsNewVersion)
          {
            model.Name = "V" + olderVersion.ToString(".#").Replace(',', '.');
            if (!model.Name.Contains('.'))
              model.Name += ".0";
          }
        }
        await unitOfWork.Repository.Add(new Version()
        {
          Name = model.Name,
          Date = DateTime.Now,
          Description = model.Description,
          DatebaseChange = model.DatabaseChange,
          ProjectId = model.ProjectId
        });
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> DeleteVersinWithProjectId(int Id)
    {
      var version = GetNonDeletedAndActive<Version>(t => t.ProjectId == Id);
      await DeleteRange(version.ToList());
      return "1";
    }

    public async Task<string> DeleteVersion(int Id)
    {
      try
      {
        var getVersion = await GetById<Version>(Id);
        var totolRecord = GetNonDeletedAndActive<Version>(t => t.ProjectId == getVersion.ProjectId).Count();
        if (totolRecord <= 1)
          return "0";
        await Delete<Version>(Id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<IQueryable<VersionDTO>> GetList()
    {
      var model = GetNonDeletedAndActive<Version>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active).Select(s => new VersionDTO()
      {
        Id = s.Id,
        Name = s.Name,
        Date = s.Date,
        ProjectId = s.ProjectId,
        Description = s.Description,
        DatabaseChange = s.DatebaseChange,
      });
      return model;
    }

    public async Task<string> GetVersion(int Id)
    {
      try
      {
        var version = GetNonDeletedAndActive<Version>(t => t.ProjectId == Id).Select(p => new VersionDTO()
        {
          Id = p.Id,
          Name = p.Name,
          Date = p.Date,
          ProjectId = p.ProjectId,
          Description = p.Description,
          DatabaseChange = p.DatebaseChange,
        });
        return JsonConvert.SerializeObject(version);
      }
      catch (Exception)
      {

        throw;
      }
    }

    public double GetVersionName(int Id)
    {
      var version = GetNonDeletedAndActive<Version>(t => t.ProjectId == Id);
      if (version.Count() != 0)
      {
        var versions = version.OrderBy(t => t.Id).Last();
        var versionName = versions.Name.Replace('-', ',').Replace("V", "");
        return Convert.ToDouble(versionName);
      }
      return 0;
    }

    public async Task<string> UpdateVersion(Version model)
    {
      Update(model);
      await unitOfWork.SaveChanges();
      return "1";
    }

    public async Task<string> UpdateVersion(VersionDTO model)
    {
      try
      {
        var olderVersion = GetVersionName(model.ProjectId);

        if (olderVersion != 0)
        {
          if (model.IsNewVersion)
          {
            model.Name = "V" + (olderVersion + 0.1).ToString(".#").Replace(',', '.');
            if (!model.Name.Contains('.'))
              model.Name += ".0";
          }
          else
            model.Name = "V" + olderVersion.ToString(".#").Replace(',','.');
        }

        var version = await GetById<Version>(model.Id);
        version.Name = model.Name;
        version.Description = model.Description;
        version.ProjectId = model.ProjectId;
        version.Date = model.Date;
        version.DatebaseChange = model.DatabaseChange;
        Update(version);
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
