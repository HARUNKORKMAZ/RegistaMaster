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
        var olderVersion = GetVersionName(model.ProjectID);
        if (olderVersion != 0)
        {
          if (model.IsNewVersion)
          {
            model.Name = "V" + (olderVersion + 0.1).ToString(".#").Replace(',', '.');
            if(!model.Name.Contains('.'))
              model.Name += ".0";
          }
          else
          {
            model.Name="V"+olderVersion.ToString(".#").Replace(',', '.');
            if(!model.Name.Contains('.'))
              model.Name += ".0";
          }
        }
        await unitOfWork.Repository.Add(new Version()
        {
          Name = model.Name,
          Date = DateTime.Now,
          Description = model.Description,
          DatabaseChange = model.DatabaseChange,
          ProjectID = model.ProjectID
        });
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task<string> DeleteVersionWithProjectId(int Id)
    {
      var version = GetNonDeletedAndActive<Version>(t => t.ProjectID == Id);
      await DeleteRange(version.ToList());
      return "1";
    }

    public async Task<string> DeleteVersion(int Id)
    {
      try
      {
        var getVersion = await GetById<Version>(Id);
        var totolRecord = GetNonDeletedAndActive<Version>(t => t.ProjectID == getVersion.ProjectID).Count();
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
        ID = s.ID,
        Name = s.Name,
        Date = s.Date,
        ProjectID = s.ProjectID,
        Description = s.Description,
        DatabaseChange = s.DatabaseChange,
      });
      return model;
    }

    public async Task<string> GetVersion(int Id)
    {
      try
      {
        var version = GetNonDeletedAndActive<Version>(t => t.ProjectID == Id).Select(p => new VersionDTO()
        {
          ID = p.ID,
          Name = p.Name,
          Date = p.Date,
          ProjectID = p.ProjectID,
          Description = p.Description,
          DatabaseChange = p.DatabaseChange,
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
      var versions = GetNonDeletedAndActive<Version>(t => t.ProjectID == Id);
      if (versions.Count() != 0)
      {
        var version = versions.OrderBy(t => t.ID).Last();
        var versionName = version.Name.Replace('.', ',').Replace("V", "");
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
        var olderVersion = GetVersionName(model.ProjectID);

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

        var version = await GetById<Version>(model.ID);
        version.Name = model.Name;
        version.Description = model.Description;
        version.ProjectID = model.ProjectID;
        version.Date = model.Date;
        version.DatabaseChange = model.DatabaseChange;
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
