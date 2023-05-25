using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class User_Service : IUser_Service
	{
		AssDbContext context;

		public User_Service()
		{
			context = new AssDbContext();
		}

		public bool Add(User_Model model)
		{
			try
			{
				context.User_Models.Add(model);
				context.SaveChanges();
				var cart = new Cart_Model();
				cart.UserId = model.Id;
				cart.Description = model.UserName;
				context.Cart_Models.Add(cart);
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false; 
			}
		}

        public User_Model GetUserLogin(string userName, string pass)
        {
            var listUser = context.User_Models.ToList();
			var check = listUser.FirstOrDefault(x => x.UserName == userName.Trim() && x.Password == pass.Trim());
			return check;
        }

		public bool CheckUserName (string userName)
		{
			var listUser = GetAll();
			var user = listUser.FirstOrDefault(x => x.UserName == userName.Trim());
			if (user != null)
				return false;
			return true;
		}

        public bool DeleteById(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<User_Model> GetAll()
		{
			return context.User_Models.ToList();
		}

		public User_Model GetById(Guid id)
		{
			return context.User_Models.Find(id);
		}

		public List<User_Model> GetByName(string name)
		{
			throw new NotImplementedException();
		}

		public bool Update(User_Model model)
		{
			throw new NotImplementedException();
		}
		public bool UpdatePass(User_Model model)
		{
			try
			{
				var user = context.User_Models.Find(model.Id);
				user.Password = model.Password;
				context.SaveChanges();
				return true;
			} 
			catch (Exception ex)
			{
				return false;
			}
		}
		
	}
}
