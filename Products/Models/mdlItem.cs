using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Models
{
    public class mdlItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
