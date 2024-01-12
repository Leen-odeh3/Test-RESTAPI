using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }

        public List<Item> Items { get; set; }
    }
}
