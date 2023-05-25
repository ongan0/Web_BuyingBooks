using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class Cart_Service : ICart_Service
	{

        AssDbContext context;

        public Cart_Service()
        {
            context = new AssDbContext();
        }

        public bool Add(Cart_Model model)
		{
            try
            {
                context.Cart_Models.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

		public bool DeleteById(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<Cart_Model> GetAll()
		{
			return context.Cart_Models.ToList();
		}

		public Cart_Model GetById(Guid id)
		{
            return context.Cart_Models.Find(id);
        }
	}
}
