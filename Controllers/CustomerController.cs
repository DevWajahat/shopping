using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shoppingCart.Models;

namespace shoppingCart.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;
        public CustomerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Category> category = _context.tbl_Category.ToList();
            ViewData["category"] = category;

            List<Product> product = _context.tbl_Product.ToList();
            ViewData["product"] = product;
            ViewBag.checkSession = HttpContext.Session.GetString("customer_session");  
            return View();
        }
        public IActionResult customerAccount()
        {
         if(string.IsNullOrEmpty(HttpContext.Session.GetString("customer_session")))
            {
                return RedirectToAction("customerLogin");
            }
            else
            {
                List<Category> category = _context.tbl_Category.ToList();
                ViewData["category"] = category;
                var customerId = HttpContext.Session.GetString("customer_session");
                var row = _context.tbl_Customer.Where(c =>c.Customer_id == int.Parse(customerId)).ToList();
                return View(row);
            }
        }
        [HttpPost]
        public IActionResult CustomerUpdateProfile(Customer customer)
        {
            _context.tbl_Customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("customerAccount");
        }

        public IActionResult changeProfileImage(Customer customer,IFormFile Customer_image)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "customer_images", Customer_image.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            Customer_image.CopyTo(fs);
            customer.Customer_image = Customer_image.FileName;
            _context.tbl_Customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("customerAccount");
        }

        public IActionResult feedback()
        {
            List<Category> category = _context.tbl_Category.ToList();
            ViewData["category"] = category;
            return View();
        }
        [HttpPost]
        public IActionResult feedback(Feedback feedback)
        {
            TempData["message"] = "Thank You for your feedback";
            _context.tbl_feedback.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("feedback");
        }


        public IActionResult customerLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult customerLogin(string Customer_email, string Customer_password)
        {
            {
                var row = _context.tbl_Customer.FirstOrDefault(a => a.Customer_email == Customer_email);
                if (row != null && row.Customer_password == Customer_password)
                {
                    HttpContext.Session.SetString("customer_session", row.Customer_id.ToString());
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.message = "Incorrect Username or Password";
                    return View();
                }
               
            }

        }
        public IActionResult customerLogout()
        {
            HttpContext.Session.Remove("customer_session");
            return RedirectToAction("Index");
        }

        public IActionResult CustomerRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerRegister(Customer customer)
        {
            _context.tbl_Customer.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("customerLogin");
        }
        public IActionResult fetchAllProducts()
        {
            List<Category> category = _context.tbl_Category.ToList();
            ViewData["category"] = category;

            List<Product> product = _context.tbl_Product.ToList();
            ViewData["product"] = product;
            return View();
        }
        public IActionResult productDetails(int id)
        {
            List<Category> category = _context.tbl_Category.ToList();
            ViewData["category"] = category;

           var productdetails = _context.tbl_Product.Where(p => p.product_id == id).ToList();

            return View(productdetails);
        }
        public IActionResult AddToCart(int product_id,Cart cart)
        {
          string isLogin =  HttpContext.Session.GetString("customer_session");
            if (isLogin != null)
            {
                cart.product_id = product_id;
                cart.customer_id = int.Parse(isLogin);
                cart.product_quantity = 1;
                cart.cart_status = 0;
                _context.tbl_Cart.Add(cart);
                _context.SaveChanges();
                TempData["message"] = "Product Successfully added in Cart";

                return RedirectToAction("fetchAllProducts");
               
            }
            else
            {
                return RedirectToAction("customerLogin");
            }
        }
        public IActionResult fetchCart()
        {
            List<Category> category = _context.tbl_Category.ToList();
            ViewData["category"] = category;

            string customerId = HttpContext.Session.GetString("customer_session");
            if (customerId != null)
            {
                var cart = _context.tbl_Cart.Where(c => c.customer_id == int.Parse(customerId)).Include(c => c.products).ToList();

                return View(cart);
            }
            else
            {
                return RedirectToAction("customerLogin");
            }
        }
        public IActionResult removeProduct(int id)
        {
           var product= _context.tbl_Cart.Find(id);
            _context.tbl_Cart.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");  
        }
    }
}
