using Core.Enitities;
using Core.Interfaces;
using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductTypesRepository:GenericRepository<ProductType>,IProductTypesRepository
    {
        public ProductTypesRepository(StoreDbContext context):base(context)
        {
            
        }
    }
}
