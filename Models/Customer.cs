using System.ComponentModel.DataAnnotations;

namespace shoppingCart.Models
{
    public class Customer
    {
        [Key]
        public int Customer_id { get; set; }
        public string Customer_name { get; set; }
        public string? Customer_phone { get; set; }
        public string Customer_email { get; set; }
        public string Customer_password { get; set; }
        public string? Customer_gender { get; set; }
        public string? Customer_country { get; set; }
        public string? Customer_city { get; set; }
        public string? Customer_address { get; set; }
        public string? Customer_image { get; set; }
    }
}
