    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shoppingCart.Models;

    namespace shoppingCart.Controllers
    {
        public class DealerController : Controller
        {
            private ApplicationDbContext _context;
            private IWebHostEnvironment _env;
            public DealerController(ApplicationDbContext context,IWebHostEnvironment env)
            {
                _context = context; 
                _env = env;
            }
            public IActionResult Index()
            {
               string admin_session = HttpContext.Session.GetString("dealer_session");
                if (admin_session != null) {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Login(string dealerEmail, string dealerPassword)
            {
             var row =   _context.tbl_Dealer.FirstOrDefault(a => a.Dealer_email == dealerEmail);
                if(row != null && row.Dealer_password == dealerPassword)
                {
                    HttpContext.Session.SetString("dealer_session", row.Dealer_id.ToString());
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.message = "Incorrect Username or Password";
                    return View();
                }
                return View();
            }
            public IActionResult logout()
            {
                HttpContext.Session.Remove("dealer_session");
                return RedirectToAction("Login");
            }
        public IActionResult Profile()
        {
            var dealerId = HttpContext.Session.GetString("dealer_session");
            if (dealerId != null)
            {
                var row = _context.tbl_Dealer.Where(a => a.Dealer_id == int.Parse(dealerId)).ToList();
                if (row == null || row.Count == 0)
                {
                    // Handle the case where the dealer is not found
                    // Redirect to an appropriate action or return an error view
                    return RedirectToAction("Login");
                }
                return View(row);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
            public IActionResult Profile(Dealer dealer)
            {
             _context.tbl_Dealer.Update(dealer);
                _context.SaveChanges();
                return RedirectToAction("Profile");
            }
            public IActionResult ChangeProfileImage(IFormFile Dealer_image,Dealer dealer)
            {
                string ImagePath = Path.Combine(_env.WebRootPath, "Dealer_image", Dealer_image.FileName);
                FileStream fs = new FileStream(ImagePath, FileMode.Create);
                Dealer_image.CopyTo(fs);
                dealer.Dealer_image = Dealer_image.FileName;
                _context.tbl_Dealer.Update(dealer);
                _context.SaveChanges();
                return RedirectToAction("Profile");
            }
            public IActionResult fetchcustomer()
            {

                return View(_context.tbl_Customer.ToList());
            }
            public IActionResult customerDetails(int id)
            {
            
                return View(_context.tbl_Customer.FirstOrDefault(c => c.Customer_id == id));
            }
            public IActionResult updateCustomer(int id)
            {
                return View(_context.tbl_Customer.Find(id));
            }
            [HttpPost]
            public IActionResult updateCustomer(int id,Customer customer,IFormFile customer_image)
            {
                string ImagePath = Path.Combine(_env.WebRootPath, "customer_images", customer_image.FileName);
                FileStream fs = new FileStream(ImagePath, FileMode.Create);
                customer_image.CopyTo(fs);
                customer.Customer_image = customer_image.FileName;
                _context.tbl_Customer.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("fetchcustomer");
            }

            public IActionResult deletePermission(int id)
            {
                return View(_context.tbl_Customer.FirstOrDefault(c => c.Customer_id == id));
            }

            public IActionResult deleteCustomer(int id)
            {
              var customer =  _context.tbl_Customer.Find(id);
                _context.tbl_Customer.Remove(customer);
                _context.SaveChanges();
                return RedirectToAction("fetchcustomer");
            }
            public IActionResult fetchcategory()
            {
                return View(_context.tbl_Category.ToList());
            }
            public IActionResult addcategory()
            {
                return View();
            }
            [HttpPost]
            public IActionResult addcategory(Category cat)
            {
                _context.tbl_Category.Add(cat);
                _context.SaveChanges();
                return RedirectToAction("fetchcategory");
            }
            public IActionResult updatecategory(int id)
            {
                var category = _context.tbl_Category.Find(id);
                return View(category);
            }
            [HttpPost]
            public IActionResult updatecategory(Category cat)
            {
                _context.tbl_Category.Update(cat);
                _context.SaveChanges();
                return RedirectToAction("fetchcategory");
            }
            public IActionResult deletePermissionCategory(int id)
            {
                return View(_context.tbl_Category.FirstOrDefault(c => c.category_id == id));
            }
            public IActionResult DeleteCategory(int id) {

              var category =  _context.tbl_Category.Find(id);
                _context.tbl_Category.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("fetchcategory");
            }
            public IActionResult fetchProduct()
            {
            return View(_context.tbl_Product.ToList());
            }
        public IActionResult addproduct()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult addproduct(Product prod,IFormFile product_image)
        {
            string imageName = Path.GetFileName(product_image.FileName);
            string imagePath = Path.Combine(_env.WebRootPath, "product_images", imageName);
            FileStream fs = new FileStream(imagePath, FileMode.Create);
            product_image.CopyTo(fs);
            prod.product_image = imageName;

            _context.tbl_Product.Add(prod);
            _context.SaveChanges();
            return RedirectToAction("fetchproduct");
        }
        public IActionResult productDetails(int id)
        {

            return View(_context.tbl_Product.Include(p => p.category).FirstOrDefault(p=>p.product_id==id));
        }
        public IActionResult deletePermissionProduct(int id)
        {
            return View(_context.tbl_Product.FirstOrDefault(p => p.product_id == id));
        }
        public IActionResult deleteProduct(int id)
        {

            var product = _context.tbl_Product.Find(id);
            _context.tbl_Product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult updateproduct(int id)
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            var product = _context.tbl_Product.Find(id);
            ViewBag.selectedCategoryId = product.cat_id;
            return View(product);
        }
        [HttpPost]
        public IActionResult updateproduct(Product product)
        {


            _context.tbl_Product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult ChangeProductImage(IFormFile product_image, Product product)
        {
                string ImagePath = Path.Combine(_env.WebRootPath, "product_images", product_image.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            product_image.CopyTo(fs);
            product.product_image = product_image.FileName;
            _context.tbl_Product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult fetchfeedback()
        {
            
            return View(_context.tbl_feedback.ToList());
        }
        public IActionResult deletePermissionFeedback(int id)
        {
            return View(_context.tbl_feedback.FirstOrDefault(f =>f.Feedback_id == id));
        }
        public IActionResult deleteFeedback(int id)
        {
            var feedback = _context.tbl_feedback.Find(id);
            _context.tbl_feedback.Remove(feedback);
            _context.SaveChanges();
            return RedirectToAction("fetchfeedback");
        }
        public IActionResult fetchCart()
        {
            var cart = _context.tbl_Cart.Include(c => c.products).Include (c => c.customers).ToList();
            return View(cart);
        }
        public IActionResult deletePermissionCart(int id)
        {
            return View(_context.tbl_Cart.FirstOrDefault(c => c.cart_id == id));
        }

       

        public IActionResult deleteCart(int id)
        {
            var cart = _context.tbl_Cart.Find(id);
            _context.tbl_Cart.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }
        public IActionResult updateCart(int id)
        {
            var cart = _context.tbl_Cart.Find(id);
           
            return View(cart);
         
        }
        [HttpPost]
        public IActionResult updateCart(int cart_status, Cart cart)
        {
            cart.cart_status = cart_status;
            _context.tbl_Cart.Update(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }
    }
}
