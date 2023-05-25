using Assignment.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace Assignment.Services
{
    public static class Session_Service
    {
        // Đưa dữ liệu vào session dưới dạng json
        public static void SetObjToJson(ISession session, string key, object value)
        {
            // obj value là dữ liệu bạn muốn thêm vào
            // b1 chuyển dữ liệu đó sang dạng json string
            var jsonString = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonString);
        }
        // lấy và đưa dữ liệu về dạng guid từ session
        public static Guid GetObjFromSessionGuid(ISession session, string key)
        {
            var data = session.GetString(key); // đọc dữ liệu từ session ở dạng chuỗi
            if (data != null)
            {
                var listObj = JsonConvert.DeserializeObject<Guid>(data);
                return listObj;
            }
            else return new Guid("00000000-0000-0000-0000-000000000000");
        }
        // lấy và đưa dữ liệu về dạng list<Product> từ session
        public static List<Product_Model> GetListProFromSession(ISession session, string key)
        {
            var data = session.GetString(key); // đọc dữ liệu từ session ở dạng chuỗi
            if (data != null)
            {
                var listObj = JsonConvert.DeserializeObject<List<Product_Model>>(data);
                return listObj;
            }
            else return new List<Product_Model>();
        }
        public static User_Model GetObjFromSessionUser(ISession session, string key)
        {
            var data = session.GetString(key); // đọc dữ liệu từ session ở dạng chuỗi
            if (data != null)
            {
                var obj = JsonConvert.DeserializeObject<User_Model>(data);
                return obj;
            }
            else return new User_Model();
        }
        public static bool CheckProductInCart(Guid id, List<Product_Model> cartProducts)
        {
            return cartProducts.Any(p => p.Id == id); // kiểm tra xem có tồn tại sp đó trong giỏ hàng chưa
        }
    }
}
