using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace  Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context) 
        {
            _context = context;
        }
        public async Task Add(T Record)
        {
            await _context.Set<T>().AddAsync(Record);
            _context.SaveChanges();
   
        }

        public async Task Delete(T record)
           => _context.Set<T>().Remove(record);
        

        public async Task<IList<T>> GetAll()
          =>  await _context.Set<T>().ToListAsync();
        

        public async Task<T> GetById(int? id)
         => await _context.Set<T>().FindAsync(id);

        public async Task Update(T Record)
        {
             _context.Set<T>().Update(Record);
             _context.SaveChanges();
        }
    }
}
