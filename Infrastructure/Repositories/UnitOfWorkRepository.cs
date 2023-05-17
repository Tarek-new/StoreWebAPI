using Core.Enitities;
using Core.Interfaces;
using Infrastructure.Contexts;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly StoreDbContext _context;

        private Hashtable _repositories;

        public UnitOfWorkRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> Commit()
       => await _context.SaveChangesAsync();



        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) { _repositories = new Hashtable(); }
            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>).MakeGenericType(type);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);

                //var repositoryInstance=Activator.CreateInstance(type);
                //_repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[type];

        }
    }
}
