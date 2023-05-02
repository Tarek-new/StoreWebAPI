

using System.Text.Json.Serialization;

namespace Core.Enitities
{
    public class Product
    {
        public int  Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }
        public virtual ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
   
        
    }
    
}
