using System.ComponentModel.DataAnnotations;

namespace shoppingCart.Models
{
    public class FAQs
    {
        [Key]
        public int faq_id { get; set; }
        public string faq_question { get; set; }
        public int faq_answer { get; set; }
    }
}
