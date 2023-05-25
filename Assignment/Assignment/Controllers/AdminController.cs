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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Web.WebPages;
using System.Text.RegularExpressions;

namespace Assignment.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IUser_Service _userService;
        private readonly IProduct_Service _product_Service;
        private readonly ICartDetail_Service _cartDetailService;
        private readonly IBillDetail_Service _billDetailService;
        private readonly IBill_Service _billService;
        private readonly ICategory_Service _categorySevice;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
            _userService = new User_Service();
            _product_Service = new Product_Service();
            _cartDetailService = new CartDetail_Service();
            _billDetailService = new BillDetail_Service();
            _billService = new Bill_Service();
            _categorySevice = new Category_Service();

        }

        public IActionResult Index()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if(userSession.RoleId.ToString()!= "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            return View();
        }

        public IActionResult ShowBillForAdmin()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            var listUser = _userService.GetAll();
            ViewData["ListUser"] = listUser;
            var listBill = _billService.GetAll();
            listBill.Reverse();
            return View(listBill);
        }
        [HttpGet]
        public IActionResult BillDetailForAdmin(Guid id)
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            var bill = _billService.GetById(id);
            var listBillDetail = _billDetailService.GetByBillId(id);
            var listProduct = _product_Service.GetAll();
            var client = _userService.GetById(bill.UserId);
            ViewData["ListProduct"] = listProduct;
            ViewData["ListBillDetail"] = listBillDetail;
            ViewData["Client"] = client;
            return View(bill);
        }
        [HttpPost]
        public IActionResult BillDetailForAdmin(Bill_Model model)
        {
            var bill = _billService.GetById(model.Id);
            var listBillDetail = _billDetailService.GetByBillId(model.Id);
            var listProduct = _product_Service.GetAll();
            var client = _userService.GetById(bill.UserId);
            ViewData["ListProduct"] = listProduct;
            ViewData["ListBillDetail"] = listBillDetail;
            ViewData["Client"] = client;
            if(bill.Status!=0 && model.Status == 0)
            {
                foreach(var item in listBillDetail)
                {
                    var product = _product_Service.GetById(item.ProductId);
                    product.AvailableQuantity += item.Quantity;
                    _product_Service.Update(product);
                }
            }else if(bill.Status==0 && model.Status != 0)
            {
                foreach (var item in listBillDetail)
                {
                    var product = _product_Service.GetById(item.ProductId);
                    product.AvailableQuantity -= item.Quantity;
                    _product_Service.Update(product);
                }
            }
            TempData["AlertMessage"] = "Trạng thái đơn hàng đã được cập nhật";
            TempData["Type"] = "alert-success";
            bill.Status = model.Status;
            _billService.Update(bill);
            return View(bill);
        }
        public IActionResult ShowCategory()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            var listCategory = _categorySevice.GetAll();
            return View(listCategory);
        }
        [HttpGet]
        public IActionResult UpdateCategory(Guid id)
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            ViewData["ListCategory"] = _categorySevice.GetAll();
            var model = _categorySevice.GetById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category_Model model)
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            var checkCate = _categorySevice.GetByName(model.CategoryName.Trim());
            if (checkCate != null && checkCate.Id != model.Id) return View(model);
            _categorySevice.Update(model);
            ViewData["ListCategory"] = _categorySevice.GetAll();
            return RedirectToAction("ShowCategory");
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category_Model model)
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            else if (userSession.RoleId.ToString() != "edd8f5a9-019a-4476-bd9a-adbce37711d6") return RedirectToAction("Products", "Product");
            var checkCate = _categorySevice.GetByName(model.CategoryName.Trim());
            if (checkCate != null)
                return View(model);
            _categorySevice.Add(model);
            return RedirectToAction("ShowCategory");
        }
    }
}
