
using Core.Enitities;
using Core.Specifications;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetById(int? id);

        Task<T> GetBySpecificationsAsync(ISpecification<T> specification);
        Task<IList<T>> GetListBySpecificationsAsync(ISpecification<T> specification);

        Task<int> CountAsync(ISpecification<T> specifications);
        Task Add(T Record);
        Task Update(T Record);
        Task Delete(T Record);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

    }
}
