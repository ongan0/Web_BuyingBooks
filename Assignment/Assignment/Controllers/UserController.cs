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

namespace Assignment.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUser_Service _userService;
        private readonly IProduct_Service _product_Service;
        private readonly ICartDetail_Service _cartDetailService;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = new User_Service();
            _product_Service = new Product_Service();
            _cartDetailService = new CartDetail_Service();
        }
        // đăng nhập
        public IActionResult Login(string userName, string password)
        {
            // nếu đã đăng nhập thì chuyển qua trang tài khoản
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            if (userSession.UserName != null) return RedirectToAction("DetailUser",userSession);
            // check tài khoản
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
                return View();
            var user = _userService.GetUserLogin(userName, password);
            if(user == null)
            {
                ViewData["warning"] = "Tài khoản hoặc mật khẩu không chính xác";
                return View();
            }
            var listCartDetail = _cartDetailService.GetByCartId(user.Id);
            if (listCartDetail != null)
            {
                var listProduct = _product_Service.GetAll();
                foreach (var cartDetail in listCartDetail)
                {
                    var product = _product_Service.GetById(cartDetail.ProductId);
                    if (product.AvailableQuantity < cartDetail.Quantity)
                    {
                        cartDetail.Quantity = product.AvailableQuantity;
                        _cartDetailService.UpdateQuantity(cartDetail);
                    }
                    else if (product.AvailableQuantity == 0)
                    {
                        _cartDetailService.DeleteById(cartDetail.Id);
                    }
                }
            }
            Session_Service.SetObjToJson(HttpContext.Session,"account",user);
            return RedirectToAction("Products","Product",null);
        }
        [HttpGet]
        // trang tài khoản
        public IActionResult DetailUser()
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login");
            return View(userSession);
        }
        // sửa mk
        public IActionResult EditPass(string oldPassword, string newPassword, string confirmPassword)
        {
            var userSession = Session_Service.GetObjFromSessionUser(HttpContext.Session, "account");
            // chưa đăng nhập thì về trang đăng nhập
            if (userSession.UserName == null) return RedirectToAction("Login");
            // vừa mở trang thì pass null => kệ mà mở trang tiếp
            if (oldPassword == null) return View();
            // check pass chứa khoảng trống
            if(oldPassword.Any(char.IsWhiteSpace) || newPassword.Any(char.IsWhiteSpace)|| confirmPassword.Any(char.IsWhiteSpace))
            {
                ViewData["CheckSpace"] = "Bạn không được nhập khoảng trống vào mật khẩu";
                return View();
            }
            // check mật khẩu mới khớp không
            if(newPassword != confirmPassword)
            {
                ViewData["CheckConfirm"] = "Mật khẩu mới không trùng khớp";
                return View();
            }
            // check mật khẩu đúng chưa
            if (oldPassword != userSession.Password)
            {
                ViewData["CheckPass"] = "Mật khẩu không chính xác";
                return View();
            }
            // check trùng mật khẩu mới và cũ
            if (oldPassword == newPassword)
            {
                ViewData["CheckOldNew"] = "Mật khẩu mới cần khác mật khẩu cũ";
                return View();
            }
            userSession.Password = newPassword;
            if (_userService.UpdatePass(userSession))
            {
                TempData["AlertMessage"] = "Bạn đã đổi mật khẩu thành công";
                Session_Service.SetObjToJson(HttpContext.Session,"account",userSession);
            }
            return View();
        }
        // đăng xuất
        public IActionResult Logout()
        {
            Session_Service.SetObjToJson(HttpContext.Session, "account", new User_Model());
            return RedirectToAction("Login");
        }
        // đăng kí
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User_Model model,string confirmPassword)
        {
            if (confirmPassword == null) return View();
            model.RoleId = new Guid("884f93b3-2d0e-44b4-8dba-8c7809a1876c");
            model.Status = 1;
            if(model.UserName.Any(char.IsWhiteSpace) || model.Password.Any(char.IsWhiteSpace) || confirmPassword.Any(char.IsWhiteSpace))
            {
                TempData["AlertMessage"] = "Tên đăng nhập, mật khẩu không được chứa dấu cách";
                TempData["Type"] = "alert-warning";
            }
            else if (confirmPassword != model.Password)
            {
                TempData["AlertMessage"] = "Mật khẩu không trùng khớp";
                TempData["Type"] = "alert-warning";
            }
            else if (!_userService.CheckUserName(model.UserName))
            {
                TempData["AlertMessage"] = "Tên đăng nhập đã tồn tại";
                TempData["Type"] = "alert-warning";
            }
            else if(_userService.Add(model))
            {
                TempData["AlertMessage"] = "Bạn đã đăng ký thành công";
                TempData["Type"] = "alert-success";
            }
            return View();
        }
        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        /// 

        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(string password, string userName)
        {
            if(String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(password))
            {
                TempData["AlertMessage"] = "Tài khoản và mật khẩu không được chứa dấu cách";
                TempData["Type"] = "alert-warning";
            }
            if(userName.Length<=6 || password.Length<=6)
            {
                TempData["AlertMessage"] = "Tài khoản và mật khẩu phải có độ dài lớn hơn 6 kí tự";
                TempData["Type"] = "alert-warning";
            }
            else
            {

                var user = _userService.GetUserLogin(userName, password);
                if (user == null)
                {
                    TempData["AlertMessage"] = "Tài khoản hoặc mật khẩu không chính xác";
                    TempData["Type"] = "alert-warning";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
