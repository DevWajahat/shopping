using System.ComponentModel.DataAnnotations;

namespace shoppingCart.Models
{
    public class Product
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string product_description { get; set; }
        public string product_image { get; set; }

        public string product_no { get; set; }
        public int cat_id { get; set; }
       
        public Category category { get; set; }
    }
}
