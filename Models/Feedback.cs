using System.ComponentModel.DataAnnotations;

namespace shoppingCart.Models
{
    public class Feedback
    {
        [Key]
        public int Feedback_id { get; set; }
        public string user_name { get; set; }
        public string user_message { get; set; }

    }
}
