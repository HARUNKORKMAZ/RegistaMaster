using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.ActionModels;
using RegistaMaster.Domain.DTOModels.Entities.RequestModels;
using RegistaMaster.Domain.DTOModels.ResponsibleHelperModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using RegistPackets.FileService.Interfaces;
using RegistPackets.FileService.Models;
using Action = RegistaMaster.Domain.Entities.Action;
using Version = RegistaMaster.Domain.Entities.Version;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class RequestRepository : Repository, IRequestRepository
  {
    private readonly RegistaMasterContext context;
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    private readonly IConfiguration configuration;
    private readonly IFileService fileService;

    public RequestRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork, IConfiguration _config, IFileService fileService) : base(_context, _session)
    {
      context = _context;
      unitOfWork = _unitOfWork;
      session = _session;
      fileService = fileService;
      configuration = _config;
    }

    public async Task<string> ActionStatusChangeUpdate(int Id, ActionStatus actionStatus)
    {
      try
      {
        var model = await unitOfWork.Repository.GetById<Action>(Id);
        model.ActionStatus = actionStatus;
        Update(model);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> AddRequestFiles(List<IFormFile> files, int Id)
    {
      try
      {
        var fileResponses = new List<FileResponseModel>();
        foreach (IFormFile file in files)
        {
          var fileResponse = fileService.SaveFile(file, "/Documents/RequestDocs");
          FileResponseModel model = new();
          model.FileName = fileResponse.FileName;
          model.Extension = fileResponse.Extension;
          model.FilePath = configuration["BasePaths:BaseUrl"] + configuration["BasePaths:ServiceUrl"] + "/" + fileResponse.FileName;
          fileResponses.Add(model);
        }

        foreach (var file in fileResponses)
        {
          await unitOfWork.Repository.Add<RequestFile>(new RequestFile()
          {
            RequestId = Id,
            FileName = file.FileName,
            FileUrl = file.FilePath,
          });
        }
        await unitOfWork.SaveChanges();
        return "fileResponces";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<SelectListItem>> CategorySelectList()
    {
      List<SelectListItem> categorySelectList = new()
      {
        new SelectListItem { Value = "0", Text = "Sınıflandrılmamış" },
        new SelectListItem { Value = "1", Text = "Yeni Fonksiyon" },
        new SelectListItem { Value = "2", Text = "Hata Giderme" },
        new SelectListItem { Value = "3", Text = "Veri Düzeltme" },
        new SelectListItem { Value = "4", Text = "Uyumluluk" },
      };
      return categorySelectList;
    }

    public async Task<string> CompleteRequest(int id)
    {
      try
      {
        var request = await unitOfWork.Repository.GetById<Request>(id);
        request.RequestStatus = RequestStatus.Closed;
        request.PlanedEndDate = DateTime.Now;
        Update<Request>(request);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public void Delete(int Id)
    {
      var request = GetNonDeletedAndActive<Request>(t => t.Id == Id);
      DeleteRange(request.ToList());
      Delete<Request>(Id);
    }

    public async Task<string> DeleteFilesWithRequestID(int id)
    {
      try
      {
        var files = GetNonDeletedAndActive<RequestFile>(t => t.RequestId == id).ToList();
        if (files.Count > 0)
          await DeleteRange<RequestFile>(files);
        return "";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> DeleteRequestFiles(List<string> filesId)
    {
      try
      {
        List<RequestFile> files = new();
        foreach (string fileId in filesId)
        {
          files.Add(await GetById<RequestFile>(Convert.ToInt32(fileId)));
        }
        await DeleteRange<RequestFile>(files);
        await unitOfWork.SaveChanges();
        return "";

      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<List<ActionDTO>> GetActionDetail(int RequestId)
    {
      var model = await GetNonDeletedAndActive<Action>(t => t.RequestId == RequestId).Select(s => new ActionDTO()
      {
        Id = s.Id,
        Description = s.Description,
        EndDate = s.EndDate,
        OpeningDate = s.OpeningDate,
        ResponsibleId = s.ResponsibleId,
        ActionStatus = s.ActionStatus,
        Subject = s.Subject,
        LastModifiedBy = s.LastModifiedBy,
        RequestId = RequestId,
        CreateOn = s.CreatedOn
      }).ToListAsync();
      return model;
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetCustomer()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> CustomerHelpers = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Customers.Where(t => true);
        foreach (var customer in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = customer.Id,
            Name = customer.Name
          };
        }
        return CustomerHelpers;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<RequestGridDTO>> GetList()
    {
      var model = GetNonDeletedAndActive<Request>(t => t.ObjectStatus == ObjectStatus.NonDeleted).OrderByDescending(t => t.Id).Select(t => new RequestGridDTO()
      {
        ID = t.Id,
        CreatedBy = t.CreatedBy,
        Subject = t.Subject,
        CategoryID = t.CategoryId,
        NotificationTypeID = t.NotificationTypeId,
        PageURL = t.PageUrl,
        PictureURL = t.PictureUrl,
        StartDate = t.StartDate,
        CreatedOn = t.CreatedOn,
        PlannetEndDate = t.PlanedEndDate,
        RequestStatus = t.RequestStatus,
        NotificationID = t.NotificationId,
        VersionID = t.VersionId,
        ModuleID = t.ModuleId,
        ProjectID = t.ProjectId,
        Description = t.Description
      }).ToList();
      return model;
    }

    public async Task<List<RequestGridDTO>> GetListWithFiles()
    {
      var requests = await context.Requests
        .Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active)
        .OrderByDescending(s => s.Id)
        .Include(x => x.Files)
        .Select(x => new RequestGridDTO()
        {
          ID = x.Id,
          CreatedBy = x.CreatedBy,
          Subject = x.Subject,
          CategoryID = x.CategoryId,
          NotificationTypeID = x.NotificationTypeId,
          PageURL = x.PageUrl,
          PictureURL = x.PictureUrl,
          StartDate = x.StartDate,
          CreatedOn = x.CreatedOn,
          PlannetEndDate = x.PlanedEndDate,
          RequestStatus = x.RequestStatus,
          NotificationID = x.NotificationId,
          VersionID = x.VersionId,
          ModuleID = x.ModuleId,
          ProjectID = x.ProjectId,
          Description = x.Description,
          Files = x.Files.Where(a => a.ObjectStatus == ObjectStatus.NonDeleted && a.Status == Status.Active).OrderByDescending(s => s.Id).ToList()
        }).ToListAsync();
      return requests;
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetModeluSelect()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> ModulesHelpers = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Modules
          .Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        foreach (var module in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = module.Id,
            Name = module.Name
          };
          ModulesHelpers.Add(helper);
        }
        return ModulesHelpers;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> GetModule()
    {
      try
      {
        return GetNonDeletedAndActive<Module>(t => true)
          .Select(s => new SelectListItem
          {
            Value = s.Id.ToString(),
            Text = s.Name
          }).ToList();
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> GetModuleList(int id)
    {
      return GetNonDeletedAndActive<Module>(t => t.ProjectId == id && t.ObjectStatus == ObjectStatus.NonDeleted)
        .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetProject()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> ResponsibleHelper = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Projects.
          Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        foreach (var item in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = item.Id,
            Name = item.ProjectName
          };
          ResponsibleHelper.Add(helper);
        }
        ;
        return ResponsibleHelper;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> GetProjectSelect()
    {
      try
      {
        return GetNonDeletedAndActive<Project>(t => true)
          .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.ProjectName }).ToList();

      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> GetVersion()
    {
      try
      {
        return GetNonDeletedAndActive<Version>(t => true)
          .Select(s => new SelectListItem
          {
            Value = s.Id.ToString(),
            Text = s.Name
          }).ToList();
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> GetVersionList(int id)
    {
      var versions = await GetNonDeletedAndActive<Version>(t => t.ProjectId == id && t.ObjectStatus == ObjectStatus.NonDeleted).ToListAsync();
      var uniqueVersions = versions.GroupBy(v => v.Name)
        .Select(g => g.First())
        .ToList();
      return uniqueVersions.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
    }

    public async Task<List<ResponsibleDevextremeSelectListHelper>> GetVersionSelect()
    {
      try
      {
        List<ResponsibleDevextremeSelectListHelper> ModuleHelpers = new List<ResponsibleDevextremeSelectListHelper>();
        var model = context.Versions
          .Where(t => true);
        foreach (var v in model)
        {
          ResponsibleDevextremeSelectListHelper helper = new ResponsibleDevextremeSelectListHelper()
          {
            Id = v.Id,
            Name = v.Name,
          };
          ModuleHelpers.Add(helper);
        }
        return ModuleHelpers;

      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> NotificationTypeSelectList()
    {
      List<SelectListItem> notificationTypeSelectList = new()
      {
        new SelectListItem{Value ="0" , Text="Hata"},
        new SelectListItem{Value ="1", Text="Öneri"}
      };
      return notificationTypeSelectList;
    }

    public async Task<Request> RequestAdd(Request model)
    {
      try
      {
        var time = DateTime.Now;

        model.StartDate = time;
        model.PlanedEndDate = time.AddDays(7);
        model.RequestStatus = RequestStatus.Open;
        var request = await unitOfWork.Repository.Add(model);
        await unitOfWork.SaveChanges();
        return request;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> RequestDelete(int id)
    {
      try
      {
        await DeleteFilesWithRequestID(id);
        await Delete<Request>(id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<string> RequestDeleteWithActions(int id)
    {
      try
      {
        var actions = GetQueryable<Action>(t => t.RequestId == id && t.ObjectStatus == ObjectStatus.NonDeleted).ToList();
        foreach (var action in actions)
        {
          await Delete<Action>(action.Id);
          var actionNotes = GetNonDeletedAndActive<ActionNote>(t => t.ActionId == action.Id).ToList();
          await DeleteRange<ActionNote>(actionNotes);
        }

        var files = GetNonDeletedAndActive<RequestFile>(t => t.RequestId == id).ToList();
        if (files.Count > 0)
          await DeleteRange<RequestFile>(files);
        await Delete<Request>(id);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<List<SelectListItem>> ResponsibleSelecetList()
    {
      var list = GetNonDeletedAndActive<User>(t => t.AuthorizationStatus != AuthorizationStatus.Admin)
        .Select(user => new SelectListItem
        {
          Value = user.Id.ToString(),
          Text = user.FullName,
        }).ToList();
      return list;
    }

    public async Task<Request> UpdateRequest(RequestGridDTO model)
    {
      var request = await GetById<Request>(model.ID);
      request.NotificationTypeId = model.NotificationTypeID;
      request.CategoryId = model.CategoryID;
      request.ProjectId = model.ProjectID;
      request.ModuleId = model.ModuleID;
      request.VersionId = model.VersionID;
      request.Subject = model.Subject;
      request.Description = model.Description;
      request.PageUrl = model.PageURL;
      request.PictureUrl = model.PictureURL;

      Update(request);
      await unitOfWork.SaveChanges();
      return request;
    }
  }
}
