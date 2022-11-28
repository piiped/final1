using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace final1.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Detail { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }


        [Required,
        DataType(DataType.Currency)]
        public int Price { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl1 { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl2 { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl3 { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl4 { get; set; }
    }
}
