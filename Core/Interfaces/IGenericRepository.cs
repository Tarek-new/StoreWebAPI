
namespace Core.Interfaces
{
    public interface IGenericRepository <T>
    { 
        Task<IList<T>> GetAll ();
        Task<T> GetById (int? id);
        Task Add ( T Record);
        Task Update ( T Record);
        Task Delete (T record);

    }
}
