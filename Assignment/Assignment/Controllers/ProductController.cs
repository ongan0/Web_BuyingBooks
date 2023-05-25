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
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace Assignment.Controllers
{
    public class ProductController :  Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProduct_Service _productService;
        private readonly ICategory_Service _categoryService;
        private readonly ICartDetail_Service _cartDetailService;

        public ProductController (ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = new Product_Service();
            _categoryService = new Category_Service();
            _cartDetailService = new CartDetail_Service();
        }


        // Các action về trang Product//////////////////////////////
        // hiển thị danh sách Product
        public IActionResult Products(Guid categoryId,string name)
        {
            Guid tempCategoryId = Session_Service.GetObjFromSessionGuid(HttpContext.Session, "tempCategoryId");

            if (categoryId.ToString() == "00000000-0000-0000-0000-000000000000" && tempCategoryId.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                categoryId = tempCategoryId;
            }
            else if (categoryId.ToString() == "00000000-0000-0000-0000-000000000000" && tempCategoryId.ToString() == "00000000-0000-0000-0000-000000000000")
                categoryId = new Guid("00000000-0000-0000-0000-000000000001");
            // đưa danh sách phân loại sp vào
            var listCategories = _categoryService.GetAll();
            ViewData["listcategories"] = listCategories;
            // lấy danh sách sp theo phân loại
            var listProducts = new List<Product_Model>();
            if (categoryId.ToString() == "00000000-0000-0000-0000-000000000001")
                listProducts = _productService.GetAllPro();
            else
                listProducts = _productService.GetByCategory(categoryId);
            //lấy ds sp theo tên từ sd sp có sẵn
            if(!String.IsNullOrEmpty(name))
                listProducts = listProducts.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            Session_Service.SetObjToJson(HttpContext.Session, "tempCategoryId",categoryId.ToString());
            return View(listProducts);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid id, int quantity)
        {
            if(quantity==0) return RedirectToAction("Cart", "Cart");
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            var listCart = _cartDetailService.GetByCartId(userSession.Id);
            var cartDetail = new CartDetail_Model();
            cartDetail.ProductId = id;
            cartDetail.Quantity = quantity;
            cartDetail.UserId = userSession.Id;
            if (listCart == null)
            {
                _cartDetailService.Add(cartDetail);
            }
            else
            {
                var productInCart = listCart.FirstOrDefault(x => x.ProductId == id);
                if (productInCart != null)
                {
                    productInCart.Quantity += quantity;
                    
                    _cartDetailService.UpdateQuantity(productInCart);
                }
                else
                {
                    _cartDetailService.Add(cartDetail);
                }
            }
            var model = _productService.GetById(id);
            //return View("ShowDetailProduct", model);
            return RedirectToAction("Cart","Cart");
            //return Json(data: new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng" }, JsonRequestBehavior.AllowGet);
        }
        public IActionResult ShowDetailProduct(Guid id)
        {
            var model = _productService.GetById(id);
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            if (userSession != null)
            {
                var listCart = _cartDetailService.GetByCartId(userSession.Id);
                if (listCart != null)
                {
                    var productInCart = listCart.FirstOrDefault(x => x.ProductId == id);
                    if (productInCart != null)
                    {
                        ViewData["QuantityCanAdd"] = model.AvailableQuantity - productInCart.Quantity;
                    }
                }
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
