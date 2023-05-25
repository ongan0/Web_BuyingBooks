using Assignment.Models;
namespace Assignment.IServices
{
	public interface IUser_Service
	{
        bool Add(User_Model model);
        bool Update(User_Model model);
        bool DeleteById(Guid id);
        public User_Model GetById(Guid id);
        public List<User_Model> GetAll();
        public List<User_Model> GetByName(string name);
        public User_Model GetUserLogin(string userName, string pass);
        bool UpdatePass(User_Model model);
        bool CheckUserName(string userName);
    }
}
