using RegistaMaster.Domain.DTOModels.SelectModels;
using RegistaMaster.Domain.Entities;
using System.Linq.Expressions;

namespace RegistaMaster.Application.Repositories
{
    public interface IRepository
    {
        Task<T> Add<T> (T _object)where T : BaseEntity;
        T Update<T>(T _object) where T : BaseEntity;
        Task<T> Delete<T>(int id) where T : BaseEntity;
        Task<ICollection<T>> UpdateRange<T>(ICollection<T> _objectList) where T : BaseEntity;
        Task<ICollection<T>> DeleteRange<T>(ICollection<T> _objectList) where T : BaseEntity;
        Task<T> GetById<T>(int id) where T : BaseEntity;
        IQueryable<T> GetList<T>(Expression<Func<T,bool>> where) where T : BaseEntity;
        IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T,bool>> expression) where T : BaseEntity;
        IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> where) where T : BaseEntity;
        List<SelectModel> GetEnumSelect<E>();
        string GetDisplayValue<E>(E value);
        string LookUpResource(Type resourceManagerProvider, string resourceKey);
    }
}
