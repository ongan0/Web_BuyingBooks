using Assignment.Models;

namespace Assignment.IServices
{
	public interface ICart_Service
	{
        bool Add(Cart_Model model);
        bool DeleteById(Guid id);
        public Cart_Model GetById(Guid id);
        public List<Cart_Model> GetAll();
    }
}
