using Assignment.IServices;
using Assignment.Services;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Grpc.Core;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct_Service _productService;
        private readonly ICategory_Service _categoryService;
        private readonly IBillDetail_Service _billDetailService;
        private readonly ICartDetail_Service _cartDetailService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = new Product_Service();
            _categoryService = new Category_Service();
            _billDetailService = new BillDetail_Service();
            _cartDetailService = new CartDetail_Service();
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // Product-----------------------------
        public IActionResult ShowProducts()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            if(userSession.Id.ToString()== "627896ae-1dec-44d7-aac1-fde287b7af13")
            {
                var listCategory = _categoryService.GetAll();
                ViewData["listCategory"] = listCategory;
                var products = _productService.GetAll();
                return View(products);
            }
            return RedirectToAction("Products","Product");
        }
        public IActionResult AddProduct()
        {
            var listCategory = _categoryService.GetAll();
            ViewData["listCategory"] = listCategory;
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product_Model model, IFormFile imageFile)
        {
            if(imageFile != null && imageFile.Length>0)// không null và không trống
            {
                // trỏ tới thư mục wwwroot để thực hiện việc copy sang
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img",imageFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // thực hiện copy ảnh sang thư mục img ở wwwroot
                    imageFile.CopyTo(stream);
                }
                // gán lại giá trị cho linkImg của đối tượng
                model.LinkImg = imageFile.FileName; // tên file ảnh đã được sao chép
            }
            if (_productService.Add(model))
            {
                return RedirectToAction("ShowProducts");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult DetailProduct(Guid id)
        {
            var listCategory = _categoryService.GetAll();
            ViewData["listCategory"] = listCategory;
            return View(_productService.GetById(id));
        }
        [HttpGet]
        public IActionResult UpdateProduct(Guid id)
        {
            var listCategory = _categoryService.GetAll();
            ViewData["listCategory"] = listCategory;
            return View(_productService.GetById(id));
        }

        public IActionResult UpdateProduct(Product_Model model)
        {
            if (_productService.Update(model))
                return RedirectToAction("ShowProducts");
            return View(model);
        }
        public IActionResult DeleteProduct(Guid id)
        {
            var productc = _cartDetailService.GetByProductId(id);
            if (productc == null)
            {
                var productb = _billDetailService.GetByProId(id);
                if (productb == null)
                {
                    if (_productService.DeleteById(id))
                        return RedirectToAction("ShowProducts");
                }
            }
            return View("Index");
        }
        //--------------------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}