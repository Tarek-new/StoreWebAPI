using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Enitities
{
    public class ProductBrand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
    }
}