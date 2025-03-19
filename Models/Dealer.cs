using System.ComponentModel.DataAnnotations;

namespace shoppingCart.Models
{
    public class Dealer
    {
        [Key]
        public int Dealer_id { get; set; }
        public string Dealer_name { get; set; }
        public string Dealer_email { get; set; }
        public string Dealer_password { get; set; }
        public string Dealer_image { get; set; }
    }
}
