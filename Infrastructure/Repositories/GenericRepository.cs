using Core.Enitities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Contexts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace  Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context) 
        {
            _context = context;
        }
        public async Task Add(T Record)
        {
            await _context.Set<T>().AddAsync(Record);
   
        }

        public async Task Delete(T record)
        {
            _context.Set<T>().Remove(record);
        }

     

        public async Task<IList<T>> GetAllAsync()
          =>  await _context.Set<T>().ToListAsync();
        

        public async Task<T> GetById(int? id)
         => await _context.Set<T>().FindAsync(id);

        public async Task Update(T Record)
        {
             _context.Set<T>().Update(Record);
        }


        //public async Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    if(includes != null)
        //        foreach (var include in includes)
        //            query = query.Include(include);

        //    return await query.SingleOrDefaultAsync(criteria);

        //}

        public async Task<T> GetBySpecificationsAsync(ISpecification<T> specfication)
       =>  await ApplySpecs(specfication).FirstOrDefaultAsync();

        public async Task<IList<T>> GetListBySpecificationsAsync(ISpecification<T> specfication)
      => await ApplySpecs(specfication).ToListAsync();


        private IQueryable<T> ApplySpecs(ISpecification<T> specification)
        => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);
        
    }
}
