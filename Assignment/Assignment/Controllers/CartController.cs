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
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IUser_Service _userService;
        private readonly IProduct_Service _product_Service;
        private readonly ICartDetail_Service _cartDetailService;
        private readonly IBillDetail_Service _billDetailService;
        private readonly IBill_Service _billService;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            _userService = new User_Service();
            _product_Service = new Product_Service();
            _cartDetailService = new CartDetail_Service();
            _billDetailService = new BillDetail_Service();
            _billService = new Bill_Service();

        }

        public IActionResult Cart()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");

            var listCart = _cartDetailService.GetByCartId(userSession.Id);
            var listProduct = _product_Service.GetAll();
            ViewData["ListProduct"] = listProduct;
            return View(listCart);
        }

        public IActionResult DeleteCartPro(Guid id) 
        {
            _cartDetailService.DeleteById(id);
            return RedirectToAction("Cart");
        }

        public IActionResult Pay()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            ViewData["FullName"] = userSession.FullName;
            ViewData["CreateDate"] = DateTime.Now.ToString("dd/mm/yyyy");
            var listCart = _cartDetailService.GetByCartId(userSession.Id);
            var listProduct = _product_Service.GetAll();
            ViewData["ListProduct"] = listProduct;
            return View(listCart);
        }
        [HttpPost]
        public IActionResult Pay(string telNumber, string address)
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login", "User");
            var listCart = _cartDetailService.GetByCartId(userSession.Id);
            // xử lí số điện thoại
            Regex regex = new Regex(@"^\d{10}$");
            if (!regex.IsMatch(telNumber))
            {
                TempData["AlertMessage"] = "Số điện thoại cần có 10 chữ số";
                TempData["Type"] = "alert-warning";
            }
            else if (telNumber.Substring(0,1)!="0")
            {
                TempData["AlertMessage"] = "Số điện thoại cần bắt đầu bằng số 0";
                TempData["Type"] = "alert-warning";
            }
            else
            {
                string phoneNumber = telNumber;
                var bill = new Bill_Model();
                bill.Id = new Guid();
                bill.Address = address;
                bill.PhoneNumber = phoneNumber;
                bill.FullName = userSession.FullName;
                bill.UserId = userSession.Id;
                bill.Status = 1;
                bill.CreateDate = DateTime.Now;
                foreach (var item in listCart)
                {
                    var billProduct = _product_Service.GetById(item.ProductId);
                    bill.Payment += item.Quantity * billProduct.Price;
                }
                if (_billService.Add(bill))
                {
                    foreach (var item in listCart)
                    {
                        var billdetail = new BillDetail_Model();
                        var billProduct = _product_Service.GetById(item.ProductId);
                        billdetail.ProductId = item.ProductId;
                        billdetail.Quantity = item.Quantity;
                        billdetail.Price = billProduct.Price;
                        billdetail.BillId = bill.Id;
                        if (_billDetailService.Add(billdetail))
                        {
                            billProduct.AvailableQuantity -= billdetail.Quantity;
                            _product_Service.Update(billProduct);
                        }
                    }
                    _cartDetailService.DeleteByCartId(userSession.Id);
                }
            return RedirectToAction("Products","Product");
            }
            ViewData["FullName"] = userSession.FullName;
            ViewData["CreateDate"] = DateTime.Now.ToString("dd/mm/yyyy");
            var listProduct = _product_Service.GetAll();
            ViewData["ListProduct"] = listProduct;
            return View(listCart);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
