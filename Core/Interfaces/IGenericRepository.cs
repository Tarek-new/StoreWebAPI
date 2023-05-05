
using Core.Specifications;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepository <T>
    { 
        Task<IList<T>> GetAllAsync ();
        Task<T> GetById (int? id);

        Task<T> GetBySpecificationsAsync(ISpecification<T> specfication);
        Task<IList<T>> GetListBySpecificationsAsync(ISpecification<T> specfication);
        Task Add ( T Record);
        Task Update ( T Record);
        Task Delete (T Record);

        //Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes=null );

    }
}
