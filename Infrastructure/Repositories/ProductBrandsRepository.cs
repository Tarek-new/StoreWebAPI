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
    public class ProductBrandsRepository : GenericRepository<ProductBrand>, IProductBrandsRepository
    {
        public ProductBrandsRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
