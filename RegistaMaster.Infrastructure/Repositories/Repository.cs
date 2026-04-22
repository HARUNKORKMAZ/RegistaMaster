using Microsoft.EntityFrameworkCore;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.DTOModels.SelectModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class Repository : IRepository
  {
    public readonly RegistaMasterContext context;
    public readonly SessionModel session;
    public Repository(RegistaMasterContext _context, SessionModel _session)
    {
      context = _context;
      session = _session;
    }
    private DbSet<T> GetTable<T>() where T : BaseEntity
    {
      return context.Set<T>();
    }
    public async Task<T> Add<T>(T _object) where T : BaseEntity
    {
      _object.CreatedBy = session.ID;
      _object.CreatedOn = DateTime.Now;
      _object.LastModifiedBy = session.ID;
      _object.LastModifiedOn = DateTime.Now;
      _object.ObjectStatus = ObjectStatus.NonDeleted;
      _object.Status = Status.Active;
      await GetTable<T>().AddAsync(_object);
      return _object;
    }

    public async Task<T> Delete<T>(int id) where T : BaseEntity
    {
      var obj = await Find<T>(t => t.ID == id);
      await Delete(obj);
      return obj;
    }

    public async Task<ICollection<T>> DeleteRange<T>(ICollection<T> _objectList) where T : BaseEntity
    {
      try
      {
        foreach (var item in _objectList)
        {
          item.LastModifiedBy = session.ID;
          item.LastModifiedOn = DateTime.Now;
          item.ObjectStatus = ObjectStatus.Deleted;
          item.Status = Status.Passive;
        }
        GetTable<T>().UpdateRange(_objectList);
        return _objectList;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<T> GetById<T>(int id) where T : BaseEntity
    {
      return await Find<T>(t => t.ID == id);
    }

    public string GetDisplayValue<E>(E value)
    {
      var fieldInfo = value.GetType().GetField(value.ToString());
      var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

      if (descriptionAttribute[0].ResourceType != null)
        return LookUpResource(descriptionAttribute[0].ResourceType, descriptionAttribute[0].Name);

      if (descriptionAttribute[0].ResourceType == null)
        return string.Empty;
      return (descriptionAttribute.Length>0 ) ? descriptionAttribute[0].Name : value.ToString();
    }


    public List<SelectModel> GetEnumSelect<E>()
    {
      try
      {
        return (Enum.GetValues(typeof(E)).Cast<E>().Select(e=> new SelectModel(){ Text= GetDisplayValue<E>(e),Value = e.ToString(), ID=Convert.ToInt32(e)})).ToList();
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public IQueryable<T> GetList<T>(Expression<Func<T, bool>> where) where T : BaseEntity
    {
      return GetTable<T>().Where(where);
    }

    public IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
    {
      try
      {
        return GetQueryable<T>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active).Where(expression);

      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> where) where T : BaseEntity
    {
      return GetTable<T>().Where(where);
    }

    public string LookUpResource(Type resourceManagerProvider, string resourceKey)
    {
      foreach(PropertyInfo staticPropert in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
      {
        if(staticPropert.PropertyType == typeof(System.Resources.ResourceManager))
        {
          System.Resources.ResourceManager resourceManager= (System.Resources.ResourceManager)staticPropert.GetValue(null, null);
          return resourceManager.GetString(resourceKey);
        }
      }
      return resourceKey;
    }

    public T Update<T>(T _object) where T : BaseEntity
    {
      _object.LastModifiedOn = DateTime.Now;
      _object.LastModifiedBy = session.ID;
      GetTable<T>().Update(_object);
      return _object;
    }

    public async Task<ICollection<T>> UpdateRange<T>(ICollection<T> _objectList) where T : BaseEntity
    {
      try
      {
        foreach(var item in _objectList)
        {
          item.LastModifiedBy = session.ID;
          item.LastModifiedOn = DateTime.Now;
        }
        GetTable<T>().UpdateRange(_objectList);
        return _objectList;
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<T> Find<T>(Expression<Func<T, bool>> where) where T : BaseEntity
    {
      try
      {
        return await GetTable<T>().FirstOrDefaultAsync(where);
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public async Task<T> Delete<T>(T model) where T : BaseEntity
    {
      try
      {
        model.ObjectStatus = ObjectStatus.Deleted;
        model.Status = Status.Passive;
        Update<T>(model);
        return model;
      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
